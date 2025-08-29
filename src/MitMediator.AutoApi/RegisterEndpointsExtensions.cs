using System.Reflection;
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
    /// <param name="disableAntiforgery">Disable antiforgery token validation.</param>
    /// <returns><see cref="IEndpointRouteBuilder"/></returns>
    public static IEndpointRouteBuilder UseAutoApi(this IEndpointRouteBuilder app, string? basePath = null, Assembly[]? requestsAssemblies = null, bool disableAntiforgery = false)
    {
        requestsAssemblies ??= AppDomain.CurrentDomain.GetAssemblies();
        var requests = RequestHelper.GetRequestsTypes(requestsAssemblies);
        foreach (var request in requests)
        {
            if (request.GetCustomAttribute<AutoApiIgnoreAttribute>() != null)
            {
                continue;
            }
            app.MapRequest(request, basePath, disableAntiforgery);
            
        }

        return app;
    }
    
    private static void MapRequest(this IEndpointRouteBuilder app, Type requestType, string? basePath, bool disableAntiforgery = false)
    {
        var attribute = requestType.GetCustomAttribute<AutoApiAttribute>();
        if (!string.IsNullOrWhiteSpace(basePath) && string.IsNullOrWhiteSpace(attribute?.CustomPattern))
        {
            RequestHelper.BasePath = basePath;
        }
        
        var pattern = RequestHelper.GetPattern(requestType);

        var responseType = RequestHelper.GetResponseType(requestType);

        Delegate requestDelegate;
        RouteHandlerBuilder routeHandlerBuilder;
        var isKeyRequest = RequestHelper.IsKeyRequest(requestType);

        var httpMethod = attribute?.HttpMethodType is HttpMethodType.Auto or null ? 
            RequestHelper.GetHttpMethod(requestType) : attribute.HttpMethodType;

        if (httpMethod is HttpMethodType.Get or HttpMethodType.Delete)
        {
            if (isKeyRequest)
            {
                requestDelegate = WithGetParamsAndKeys(requestType, responseType);
            }
            else
            {
                var methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithGetParams),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType);

                requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;
            }
        }
        else
        {
            if (isKeyRequest)
            {
                requestDelegate = GetDelegateEndpointWithBodyAndKeys(requestType, responseType, httpMethod);
            }
            else
            {
                string methodName;
                if (typeof(IFileRequest).IsAssignableFrom(requestType))
                {
                    methodName = httpMethod is HttpMethodType.Post or HttpMethodType.PostCreate
                        ? nameof(EndpointsMethods.FormWithFile)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodName = nameof(EndpointsMethods.WithBody);
                }

                var methodInfo = typeof(EndpointsMethods)
                    .GetMethod(methodName,  BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType);

                requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;
            }
        }
        switch (httpMethod)
        {
            case HttpMethodType.Get:
                routeHandlerBuilder = app.MapGet(pattern, requestDelegate)
                        .WithOpenApi(op =>
                        {
                            var queryParams = OpenApiParameterGenerator.GenerateFromType(requestType);
                            foreach (var openApiParameter in queryParams)
                            {
                                op.Parameters.Add(openApiParameter);
                            }
                            return op;
                        })
                    .Produces(StatusCodes.Status200OK, responseType)
                    ;
                break;
            case HttpMethodType.Put:
                routeHandlerBuilder = app.MapPut(pattern, requestDelegate)
                    .Produces(StatusCodes.Status200OK, responseType);
                break;
            case HttpMethodType.Post:
            case HttpMethodType.PostCreate:
                routeHandlerBuilder = app.MapPost(pattern, requestDelegate)
                    .Produces(StatusCodes.Status200OK, responseType);
                break;
            case HttpMethodType.Delete:
                routeHandlerBuilder = app.MapDelete(pattern, requestDelegate)
                    .Produces(StatusCodes.Status200OK, responseType);
                break;
            default:
                throw new NotSupportedException($"Http method {httpMethod} is not supported");
        }

        var tag = RequestHelper.GetPluralizedTag(requestType);
        if (!string.IsNullOrWhiteSpace(tag))
        {
            tag = char.ToUpper(tag[0]) + tag[1..];
            routeHandlerBuilder = routeHandlerBuilder.WithTags(tag);
        }

        routeHandlerBuilder.WithDescription(!string.IsNullOrWhiteSpace(attribute?.Description)
            ? attribute.Description
            : $"{RequestHelper.GetHttpMethod(requestType).ToString().ToUpperInvariant()} {RequestHelper.GetPattern(requestType)}");

        routeHandlerBuilder.WithGroupName(!string.IsNullOrWhiteSpace(attribute?.Version) ? attribute.Version : "v1");

        if (disableAntiforgery)
        {
            routeHandlerBuilder.DisableAntiforgery();
        }
    }

    private static Delegate WithGetParamsAndKeys(Type requestType, Type responseType)
    {
        MethodInfo methodInfo = null;
        var keysCount = RequestHelper.GetKeysCount(requestType);
        switch (keysCount)
        {
            case 1:
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd1Key),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, RequestHelper.GetKeyType(requestType));
                break;
            case 2:
                var keys2 = RequestHelper.GetKey2Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd2Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys2.Item1, keys2.Item2);
                break;
            case 3:
                var keys3 = RequestHelper.GetKey3Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd3Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys3.Item1, keys3.Item2, keys3.Item3);
                break;
            case 4:
                var keys4 = RequestHelper.GetKey4Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd4Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys4.Item1, keys4.Item2, keys4.Item3, keys4.Item4);
                break;
            case 5:
                var keys5 = RequestHelper.GetKey5Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd5Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys5.Item1, keys5.Item2, keys5.Item3, keys5.Item4,
                        keys5.Item5);
                break;
            case 6:
                var keys6 = RequestHelper.GetKey6Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd6Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys6.Item1, keys6.Item2, keys6.Item3, keys6.Item4,
                        keys6.Item5, keys6.Item6);
                break;
            case 7:
                var keys7 = RequestHelper.GetKey7Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithGetParamsAnd7Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys7.Item1, keys7.Item2, keys7.Item3, keys7.Item4,
                        keys7.Item5, keys7.Item6, keys7.Item7);
                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;
        return requestDelegate;
    }

    private static Delegate GetDelegateEndpointWithBodyAndKeys(Type requestType, Type responseType, HttpMethodType httpMethodType)
    {
        MethodInfo methodInfo = null;
        var keysCount = RequestHelper.GetKeysCount(requestType);
        switch (keysCount)
        {
            case 1:
                if (typeof(IFileRequest).IsAssignableFrom(requestType))
                {
                    methodInfo = httpMethodType is HttpMethodType.Post or HttpMethodType.PostCreate
                        ? typeof(EndpointsMethods)
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd1Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestType, responseType, RequestHelper.GetKeyType(requestType))
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = typeof(EndpointsMethods)
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd1Key),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestType, responseType, RequestHelper.GetKeyType(requestType));
                }
                
                break;
            case 2:
                var keys2 = RequestHelper.GetKey2Type(requestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestType))
                {
                    methodInfo = httpMethodType is HttpMethodType.Post or HttpMethodType.PostCreate
                        ? typeof(EndpointsMethods)
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd2Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestType, responseType, keys2.Item1, keys2.Item2)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = typeof(EndpointsMethods)
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd2Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestType, responseType, keys2.Item1, keys2.Item2);
                }
                break;
            case 3:
                var keys3 = RequestHelper.GetKey3Type(requestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestType))
                {
                    methodInfo = httpMethodType is HttpMethodType.Post or HttpMethodType.PostCreate
                        ? typeof(EndpointsMethods)
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd3Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestType, responseType, keys3.Item1, keys3.Item2, keys3.Item3)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = typeof(EndpointsMethods)
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd3Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestType, responseType, keys3.Item1, keys3.Item2, keys3.Item3);
                }

                break;
            case 4:
                var keys4 = RequestHelper.GetKey4Type(requestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestType))
                {
                    methodInfo = httpMethodType is HttpMethodType.Post or HttpMethodType.PostCreate
                        ? typeof(EndpointsMethods)
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd4Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestType, responseType, keys4.Item1, keys4.Item2, keys4.Item3,
                                keys4.Item4)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = typeof(EndpointsMethods)
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd4Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestType, responseType, keys4.Item1, keys4.Item2, keys4.Item3,
                            keys4.Item4);
                }

                break;
            case 5:
                var keys5 = RequestHelper.GetKey5Type(requestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestType))
                {
                    methodInfo = httpMethodType is HttpMethodType.Post or HttpMethodType.PostCreate
                        ? typeof(EndpointsMethods)
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd5Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestType, responseType, keys5.Item1, keys5.Item2, keys5.Item3,
                                keys5.Item4, keys5.Item5)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = typeof(EndpointsMethods)
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd5Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestType, responseType, keys5.Item1, keys5.Item2, keys5.Item3,
                            keys5.Item4,
                            keys5.Item5);
                }

                break;
            case 6:
                var keys6 = RequestHelper.GetKey6Type(requestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestType))
                {
                    methodInfo = httpMethodType is HttpMethodType.Post or HttpMethodType.PostCreate
                        ? typeof(EndpointsMethods)
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd6Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestType, responseType, keys6.Item1, keys6.Item2, keys6.Item3,
                                keys6.Item4, keys6.Item5, keys6.Item6)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = typeof(EndpointsMethods)
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd6Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestType, responseType, keys6.Item1, keys6.Item2, keys6.Item3,
                            keys6.Item4,
                            keys6.Item5, keys6.Item6);
                }

                break;
            case 7:
                var keys7 = RequestHelper.GetKey7Type(requestType);
                if (typeof(IFileRequest).IsAssignableFrom(requestType))
                {
                    methodInfo = httpMethodType is HttpMethodType.Post or HttpMethodType.PostCreate
                        ? typeof(EndpointsMethods)
                            .GetMethod(nameof(EndpointsMethods.FormWithFileAnd7Key),
                                BindingFlags.Static | BindingFlags.NonPublic)!
                            .MakeGenericMethod(requestType, responseType, keys7.Item1, keys7.Item2, keys7.Item3,
                                keys7.Item4, keys7.Item5, keys7.Item6, keys7.Item7)
                        : throw new NotImplementedException(
                            "IFileRequest can only be used with HTTP POST requests due to multipart/form-data limitations.");
                }
                else
                {
                    methodInfo = typeof(EndpointsMethods)
                        .GetMethod(nameof(EndpointsMethods.WithBodyAnd7Keys),
                            BindingFlags.Static | BindingFlags.NonPublic)!
                        .MakeGenericMethod(requestType, responseType, keys7.Item1, keys7.Item2, keys7.Item3,
                            keys7.Item4,
                            keys7.Item5, keys7.Item6, keys7.Item7);
                }

                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;
        return requestDelegate;
    }
}