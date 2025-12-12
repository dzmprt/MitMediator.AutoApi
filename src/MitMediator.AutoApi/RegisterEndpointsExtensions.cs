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
                routeHandlerBuilder = app.MapGet(requestInfo.PatternWithTypesOfRoutParams, requestDelegate)
                        .AddOpenApiOperationTransformer((operation, context, ct) =>
                        {
                            if (operation.Parameters is null)
                            {
                                operation.Parameters = new List<IOpenApiParameter>();
                            }

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
                routeHandlerBuilder = app.MapPut(requestInfo.PatternWithTypesOfRoutParams, requestDelegate)
                    .Produces(StatusCodes.Status200OK, requestInfo.ResponseType);
                break;
            case MethodType.Post:
            case MethodType.PostCreate:
                routeHandlerBuilder = app.MapPost(requestInfo.PatternWithTypesOfRoutParams, requestDelegate)
                    .Produces(StatusCodes.Status200OK, requestInfo.ResponseType);
                break;
            case MethodType.Delete:
                routeHandlerBuilder = app.MapDelete(requestInfo.PatternWithTypesOfRoutParams, requestDelegate)
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

    private static Delegate WithGetParamsAndKeys(RequestInfo requestInfo, Type endpointsType)
    {
        MethodInfo methodInfo = null;
        var keyTypes = RequestInfo.GetKeysTypes(requestInfo.RequestType);
        switch (requestInfo.KeysCount)
        {
            case 1:
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd1Key),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0]);
                break;
            case 2:
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd2Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1]);
                break;
            case 3:
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd3Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                        keyTypes[2]);
                break;
            case 4:
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd4Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                        keyTypes[2], keyTypes[3]);
                break;
            case 5:
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd5Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                        keyTypes[2], keyTypes[3], keyTypes[4]);
                break;
            case 6:
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd6Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                        keyTypes[2], keyTypes[3], keyTypes[4], keyTypes[5]);
                break;
            case 7:
                methodInfo = endpointsType
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd7Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                        keyTypes[2], keyTypes[3], keyTypes[4], keyTypes[5], keyTypes[6]);
                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, [requestInfo])!;
        return requestDelegate;
    }

    private static Delegate GetDelegateEndpointWithBodyAndKeys(RequestInfo requestInfo, Type endpointsType)
    {
        MethodInfo methodInfo = null;
        var keyTypes = RequestInfo.GetKeysTypes(requestInfo.RequestType);
        switch (requestInfo.KeysCount)
        {
            case 1:
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd1Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0])
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd1Key),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0]);
                }

                break;
            case 2:
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd2Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0],
                                keyTypes[1])
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd2Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1]);
                }

                break;
            case 3:
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd3Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0],
                                keyTypes[1], keyTypes[2])
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd3Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                            keyTypes[2]);
                }
                break;
            case 4:
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd4Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                                keyTypes[2],  keyTypes[3])
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd4Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                            keyTypes[2],  keyTypes[3]);
                }

                break;
            case 5:
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd5Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                                keyTypes[2],  keyTypes[3], keyTypes[4])
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd5Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                            keyTypes[2],  keyTypes[3], keyTypes[4]);
                }

                break;
            case 6:
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd6Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                                keyTypes[2],  keyTypes[3], keyTypes[4],  keyTypes[5])
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd6Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                            keyTypes[2],  keyTypes[3], keyTypes[4],  keyTypes[5]);
                }

                break;
            case 7:
                if (typeof(IFileRequest).IsAssignableFrom(requestInfo.RequestType))
                {
                    methodInfo = requestInfo.MethodType is MethodType.Post or MethodType.PostCreate
                        ? endpointsType
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd7Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                                keyTypes[2],  keyTypes[3], keyTypes[4],  keyTypes[5], keyTypes[6])
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = endpointsType
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd7Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestInfo.RequestType, requestInfo.ResponseType, keyTypes[0], keyTypes[1],
                            keyTypes[2],  keyTypes[3], keyTypes[4],  keyTypes[5], keyTypes[6]);
                }

                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, [requestInfo])!;
        return requestDelegate;
    }
}