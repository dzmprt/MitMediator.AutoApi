using System.Reflection;
using Microsoft.OpenApi;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

public static class RegisterEndpointsExtensions
{
    /// <summary>
    /// Register all IRequest as minimal API endpoints.
    /// </summary>
    /// <param name="app"><see cref="IEndpointRouteBuilder"/>.</param>
    /// <param name="basePath">Base path (for example, "api")</param>
    /// <param name="requestsAssemblies">Assembly to scan.</param>
    /// <returns><see cref="IEndpointRouteBuilder"/></returns>
    public static WebApplication UseAutoApi(this WebApplication app, string? basePath = null,
        Assembly[]? requestsAssemblies = null)
    {
        requestsAssemblies ??= AppDomain.CurrentDomain.GetAssemblies();
        var requestsInfos = RequestHelper.GetRequestsInfos(requestsAssemblies, basePath);
        foreach (var request in requestsInfos)
        {
            if (request.IsIgnored)
            {
                continue;
            }

            app.MapRequest(request);
        }

        return app;
    }

    private static void MapRequest(this WebApplication app, RequestInfo requestInfo)
    {
        Delegate requestDelegate;
        RouteHandlerBuilder routeHandlerBuilder;

        Type endpointsType;
        if (app.Services.IsRequestHandlerTaskValueRegistered(requestInfo.RequestType))
        {
            endpointsType = typeof(EndpointsMethods);
        }
        else
        {
            if (app.Services.IsRequestHandlerTaskRegistered(requestInfo.RequestType))
            {
                endpointsType = typeof(EndpointsMethodsForTaskHandlers);
            }
            else
            {
                return;
            }
        }


        if (requestInfo.MethodType is MethodType.Get or MethodType.Delete)
        {
            if (requestInfo.IsKeyRequest)
            {
                requestDelegate = WithGetParamsAndKeys(requestInfo, endpointsType);
            }
            else
            {
                var methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParams),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType);

                requestDelegate = (Delegate)methodInfo.Invoke(null, [requestInfo])!;
            }
        }
        else
        {
            if (requestInfo.IsKeyRequest)
            {
                requestDelegate = GetDelegateEndpointWithBodyAndKeys(requestInfo, endpointsType);
            }
            else
            {
                string methodName;
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodName = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? nameof(EndpointsMethods.FormWithFile)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodName = nameof(EndpointsMethods.WithBody);
                }

                var methodInfo = endpointsType
                    .GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType);

                requestDelegate = (Delegate)methodInfo.Invoke(null, [requestInfo])!;
            }
        }

        switch (requestInfo.MethodType)
        {
            case MethodType.Get:
                routeHandlerBuilder = app.MapGet(requestInfo.Pattern, requestDelegate)
                        .AddOpenApiOperationTransformer((operation, context, ct) =>
                        {
                            if (operation.Parameters is null)
                            {
                                operation.Parameters = new List<IOpenApiParameter>();
                            }

                            FixNumbersInPathParams(operation);

                            var queryParams = OpenApiParameterGenerator.GenerateFromType(requestInfo.RequestType);
                            foreach (var openApiParameter in queryParams)
                            {
                                operation.Parameters.Add(openApiParameter);
                            }

                            return Task.CompletedTask;
                        })
                        .Produces(StatusCodes.Status200OK, requestInfo.ResponseType)
                    ;
                break;
            case MethodType.Put:
                routeHandlerBuilder = app.MapPut(requestInfo.Pattern, requestDelegate)
                    .AddOpenApiOperationTransformer((operation, context, ct) =>
                    {
                        FixNumbersInPathParams(operation);
                        return Task.CompletedTask;
                    })
                    .Produces(StatusCodes.Status200OK, requestInfo.ResponseType);
                break;
            case MethodType.Post:
            case MethodType.PostCreate:
                routeHandlerBuilder = app.MapPost(requestInfo.Pattern, requestDelegate)
                    .AddOpenApiOperationTransformer((operation, context, ct) =>
                    {
                        FixNumbersInPathParams(operation);
                        return Task.CompletedTask;
                    })
                    .Produces(StatusCodes.Status200OK, requestInfo.ResponseType);
                break;
            case MethodType.Delete:
                routeHandlerBuilder = app.MapDelete(requestInfo.Pattern, requestDelegate)
                    .AddOpenApiOperationTransformer((operation, context, ct) =>
                    {
                        FixNumbersInPathParams(operation);
                        return Task.CompletedTask;
                    })
                    .Produces(StatusCodes.Status200OK, requestInfo.ResponseType);
                break;
            default:
                throw new NotSupportedException($"Http method {requestInfo.MethodType} is not supported");
        }

        var pluralizedTag = requestInfo.PluralizedTag;
        if (!string.IsNullOrWhiteSpace(pluralizedTag))
        {
            pluralizedTag = char.ToUpper(pluralizedTag[0]) + pluralizedTag[1..];
            routeHandlerBuilder = routeHandlerBuilder.WithTags(pluralizedTag);
        }

        routeHandlerBuilder.WithDescription(!string.IsNullOrWhiteSpace(requestInfo.Description)
            ? requestInfo.Description
            : $"{requestInfo.MethodType.ToString().ToUpperInvariant()} {requestInfo.Pattern}");

        routeHandlerBuilder.WithGroupName(requestInfo.Version);

        if (requestInfo.IsDisableAntiforgery)
        {
            routeHandlerBuilder.DisableAntiforgery();
        }
    }

    private static void FixNumbersInPathParams(OpenApiOperation operation)
    {
        if (operation.Parameters is null)
        {
            return;
        }
        for (var i = 0; i < operation.Parameters.Count; i++)
        {
            var operationParameter = operation.Parameters[i];
            if (operationParameter.In == ParameterLocation.Path)
            {
                var temp = operationParameter.Schema?.Type;
                operation.Parameters[i] = new OpenApiParameter()
                {
                    Name = operationParameter.Name,
                    In = ParameterLocation.Path,
                    Required = operationParameter.Required,
                    Schema = new OpenApiSchema
                    {
                        Type = operationParameter.Schema?.Type?.HasFlag(JsonSchemaType.Integer) ==
                               true &&
                               operationParameter.Schema?.Type?.HasFlag(JsonSchemaType.String) ==
                               true
                            ? (operationParameter.Schema?.Type?.HasFlag(JsonSchemaType.Number) ==
                               true
                                ? JsonSchemaType.Number
                                : JsonSchemaType.Integer)
                            : operationParameter.Schema?.Type,
                        Format = operationParameter.Schema?.Format
                    }
                };
            }
        }
    }

    private static Delegate WithGetParamsAndKeys(RequestInfo requestInfo, Type endpointsType)
    {
        MethodInfo methodInfo = null;
        switch (requestInfo.KeysCount)
        {
            case 1:
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd1Key),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType,
                        RequestInfo.GetKeyType(requestInfo.RequestType));
                break;
            case 2:
                var keys2 = RequestInfo.GetKey2Type(requestInfo.RequestType);
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd2Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys2.Item1, keys2.Item2);
                break;
            case 3:
                var keys3 = RequestInfo.GetKey3Type(requestInfo.RequestType);
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd3Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys3.Item1, keys3.Item2,
                        keys3.Item3);
                break;
            case 4:
                var keys4 = RequestInfo.GetKey4Type(requestInfo.RequestType);
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd4Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys4.Item1, keys4.Item2,
                        keys4.Item3, keys4.Item4);
                break;
            case 5:
                var keys5 = RequestInfo.GetKey5Type(requestInfo.RequestType);
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd5Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys5.Item1, keys5.Item2,
                        keys5.Item3, keys5.Item4,
                        keys5.Item5);
                break;
            case 6:
                var keys6 = RequestInfo.GetKey6Type(requestInfo.RequestType);
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd6Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys6.Item1, keys6.Item2,
                        keys6.Item3, keys6.Item4,
                        keys6.Item5, keys6.Item6);
                break;
            case 7:
                var keys7 = RequestInfo.GetKey7Type(requestInfo.RequestType);
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd7Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys7.Item1, keys7.Item2,
                        keys7.Item3, keys7.Item4,
                        keys7.Item5, keys7.Item6, keys7.Item7);
                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, [requestInfo])!;
        return requestDelegate;
    }

    private static Delegate GetDelegateEndpointWithBodyAndKeys(RequestInfo requestInfo, Type endpointsType)
    {
        MethodInfo methodInfo = null;
        switch (requestInfo.KeysCount)
        {
            case 1:
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd1Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType,
                                RequestInfo.GetKeyType(requestInfo.RequestType))
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd1Key),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType,
                            RequestInfo.GetKeyType(requestInfo.RequestType));
                }

                break;
            case 2:
                var keys2 = RequestInfo.GetKey2Type(requestInfo.RequestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd2Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys2.Item1,
                                keys2.Item2)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd2Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys2.Item1, keys2.Item2);
                }

                break;
            case 3:
                var keys3 = RequestInfo.GetKey3Type(requestInfo.RequestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd3Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys3.Item1,
                                keys3.Item2, keys3.Item3)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd3Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys3.Item1, keys3.Item2,
                            keys3.Item3);
                }

                break;
            case 4:
                var keys4 = RequestInfo.GetKey4Type(requestInfo.RequestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd4Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys4.Item1,
                                keys4.Item2, keys4.Item3,
                                keys4.Item4)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd4Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys4.Item1, keys4.Item2,
                            keys4.Item3,
                            keys4.Item4);
                }

                break;
            case 5:
                var keys5 = RequestInfo.GetKey5Type(requestInfo.RequestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd5Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys5.Item1,
                                keys5.Item2, keys5.Item3,
                                keys5.Item4, keys5.Item5)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd5Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys5.Item1, keys5.Item2,
                            keys5.Item3,
                            keys5.Item4,
                            keys5.Item5);
                }

                break;
            case 6:
                var keys6 = RequestInfo.GetKey6Type(requestInfo.RequestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd6Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys6.Item1,
                                keys6.Item2, keys6.Item3,
                                keys6.Item4, keys6.Item5, keys6.Item6)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd6Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys6.Item1, keys6.Item2,
                            keys6.Item3,
                            keys6.Item4,
                            keys6.Item5, keys6.Item6);
                }

                break;
            case 7:
                var keys7 = RequestInfo.GetKey7Type(requestInfo.RequestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd7Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys7.Item1,
                                keys7.Item2, keys7.Item3,
                                keys7.Item4, keys7.Item5, keys7.Item6, keys7.Item7)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd7Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keys7.Item1, keys7.Item2,
                            keys7.Item3,
                            keys7.Item4,
                            keys7.Item5, keys7.Item6, keys7.Item7);
                }

                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, [requestInfo])!;
        return requestDelegate;
    }
}