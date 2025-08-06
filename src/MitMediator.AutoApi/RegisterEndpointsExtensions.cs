using System.Reflection;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

public static class RegisterEndpointsExtensions
{
    /// <summary>
    /// Register all IRequest as minimal API endpoints.
    /// </summary>
    /// <param name="app"><see cref="IEndpointRouteBuilder"/>.</param>
    /// <param name="basePath">Base path (for example "api")</param>
    /// <param name="requestsAssemblies">Assembly to scan.</param>
    /// <returns><see cref="IEndpointRouteBuilder"/></returns>
    public static IEndpointRouteBuilder UseAutoApi(this IEndpointRouteBuilder app, string? basePath = null, Assembly[]? requestsAssemblies = null)
    {
        requestsAssemblies ??= AppDomain.CurrentDomain.GetAssemblies();
        var requests = RequestHelper.GetRequestsTypes(requestsAssemblies);
        foreach (var request in requests)
        {
            if (request.GetCustomAttribute<AutoApiIgnoreAttribute>() != null)
            {
                continue;
            }
            app.MapRequest(request, basePath);
        }

        return app;
    }
    
    private static void MapRequest(this IEndpointRouteBuilder app, Type requestType, string? basePath)
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
                requestDelegate = GetDelegateEndpointWithBodyAndKeys(requestType, responseType);
            }
            else
            {
                var methodName = nameof(EndpointsMethods.WithBody);
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
                    .Produces(StatusCodes.Status200OK, responseType);
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

        if (!string.IsNullOrWhiteSpace(attribute?.Version))
        {
            routeHandlerBuilder.WithGroupName(attribute.Version);
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

    private static Delegate GetDelegateEndpointWithBodyAndKeys(Type requestType, Type responseType)
    {
        MethodInfo methodInfo = null;
        var keysCount = RequestHelper.GetKeysCount(requestType);
        switch (keysCount)
        {
            case 1:
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithBodyAnd1Key),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, RequestHelper.GetKeyType(requestType));
                break;
            case 2:
                var keys2 = RequestHelper.GetKey2Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithBodyAnd2Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys2.Item1, keys2.Item2);
                break;
            case 3:
                var keys3 = RequestHelper.GetKey3Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithBodyAnd3Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys3.Item1, keys3.Item2, keys3.Item3);
                break;
            case 4:
                var keys4 = RequestHelper.GetKey4Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithBodyAnd4Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys4.Item1, keys4.Item2, keys4.Item3, keys4.Item4);
                break;
            case 5:
                var keys5 = RequestHelper.GetKey5Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithBodyAnd5Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys5.Item1, keys5.Item2, keys5.Item3, keys5.Item4,
                        keys5.Item5);
                break;
            case 6:
                var keys6 = RequestHelper.GetKey6Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithBodyAnd6Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys6.Item1, keys6.Item2, keys6.Item3, keys6.Item4,
                        keys6.Item5, keys6.Item6);
                break;
            case 7:
                var keys7 = RequestHelper.GetKey7Type(requestType);
                methodInfo = typeof(EndpointsMethods)
                    .GetMethod(nameof(EndpointsMethods.WithBodyAnd7Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys7.Item1, keys7.Item2, keys7.Item3, keys7.Item4,
                        keys7.Item5, keys7.Item6, keys7.Item7);
                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;
        return requestDelegate;
    }
}