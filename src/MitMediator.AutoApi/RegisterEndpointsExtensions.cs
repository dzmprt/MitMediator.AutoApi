using System.Reflection;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

public static class RegisterEndpointsExtensions
{
    internal static void ThrowIfIsNotRequestType(Type type)
    {
        var interfaces = type.GetInterfaces();
        foreach (var iface in interfaces)
        {
            if (iface.IsGenericType &&
                iface.GetGenericTypeDefinition() == typeof(IRequest<>))
            {
                return;
            }
        }

        throw new Exception("The type " + type.FullName + " is not a IRequest or IRequest<>");
    }

    public static IEndpointRouteBuilder UseAutoApi(this IEndpointRouteBuilder app)
    {
        var createRequests = Helpers.GetTypesWithAttribute<CreateAttribute>();
        foreach (var request in createRequests)
        {
            ThrowIfIsNotRequestType(request);
            app.MapRequestCreate(request);
        }

        var createByKeyRequests = Helpers.GetTypesWithAttribute<CreateByKeyAttribute>();
        foreach (var request in createByKeyRequests)
        {
            ThrowIfIsNotRequestType(request);
            app.MapRequestCreateByKey(request);
        }

        var postRequests = Helpers.GetTypesWithAttribute<PostAttribute>();
        foreach (var request in postRequests)
        {
            ThrowIfIsNotRequestType(request);
            app.MapRequestPost(request);
        }

        var postByKeyRequests = Helpers.GetTypesWithAttribute<PostByKeyAttribute>();
        foreach (var request in postByKeyRequests)
        {
            ThrowIfIsNotRequestType(request);
            app.MapRequestPostByKey(request);
        }

        var updateRequests = Helpers.GetTypesWithAttribute<UpdateAttribute>();
        foreach (var request in updateRequests)
        {
            ThrowIfIsNotRequestType(request);
            app.MapRequestUpdate(request);
        }

        var updateByKeyRequests = Helpers.GetTypesWithAttribute<UpdateByKeyAttribute>();
        foreach (var request in updateByKeyRequests)
        {
            ThrowIfIsNotRequestType(request);
            app.MapRequestUpdateByKey(request);
        }

        var deleteRequests = Helpers.GetTypesWithAttribute<DeleteAttribute>();
        foreach (var request in deleteRequests)
        {
            app.MapRequestDelete(request);
        }

        var deleteByKeyRequests = Helpers.GetTypesWithAttribute<DeleteByKeyAttribute>();
        foreach (var request in deleteByKeyRequests)
        {
            ThrowIfIsNotRequestType(request);
            app.MapRequestDeleteByKey(request);
        }

        var getRequests = Helpers.GetTypesWithAttribute<GetAttribute>();
        foreach (var request in getRequests)
        {
            ThrowIfIsNotRequestType(request);
            app.MapRequestGet(request);
        }

        var getByKeyRequests = Helpers.GetTypesWithAttribute<GetByKeyAttribute>();
        foreach (var request in getByKeyRequests)
        {
            ThrowIfIsNotRequestType(request);
            app.MapRequestGetByKey(request);
        }

        return app;
    }

    private static void MapRequestUpdateByKey(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<UpdateByKeyAttribute>()!;
        var pattern = KeysRequestHelper.GetKeyPattern(createAttribute, requestType);
        var responseType = Helpers.GetResponseType(requestType);

        var requestDelegate = GetDelegateEndpointWithBodyAndKeys(requestType, responseType);

        var routeHandlerBuilder = app.MapPut(pattern, requestDelegate)
            .WithTags(createAttribute.Tag)
            .Produces(StatusCodes.Status200OK, responseType);

        if (!string.IsNullOrWhiteSpace(createAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(createAttribute.Version);
        }
    }

    private static void MapRequestUpdate(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<UpdateAttribute>()!;
        var pattern = Helpers.GetBasePattern(createAttribute);
        var responseType = Helpers.GetResponseType(requestType);

        var methodInfo = typeof(BuildEndpointsMethods)
            .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBody),
                BindingFlags.Static | BindingFlags.NonPublic)!
            .MakeGenericMethod(requestType, responseType);

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;

        var routeHandlerBuilder = app.MapPut(pattern, requestDelegate)
            .WithTags(createAttribute.Tag)
            .Produces(StatusCodes.Status200OK, responseType);

        if (!string.IsNullOrWhiteSpace(createAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(createAttribute.Version);
        }
    }

    private static void MapRequestCreate(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<CreateAttribute>()!;
        var pattern = Helpers.GetBasePattern(createAttribute);
        var responseType = Helpers.GetResponseType(requestType);

        var methodInfo = typeof(BuildEndpointsMethods)
            .GetMethod(nameof(BuildEndpointsMethods.BuildCreateEndpoint),
                BindingFlags.Static | BindingFlags.NonPublic)!
            .MakeGenericMethod(requestType, responseType);

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;

        var routeHandlerBuilder = app.MapPost(pattern, requestDelegate)
            .WithTags(createAttribute.Tag)
            .Produces(StatusCodes.Status201Created, responseType);

        if (!string.IsNullOrWhiteSpace(createAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(createAttribute.Version);
        }
    }

    private static void MapRequestCreateByKey(this IEndpointRouteBuilder app, Type requestType)
    {
        var postAttribute = requestType.GetCustomAttribute<CreateByKeyAttribute>()!;
        var pattern = KeysRequestHelper.GetKeyPattern(postAttribute, requestType);
        var responseType = Helpers.GetResponseType(requestType);

        var requestDelegate = GetCreateDelegateEndpointWithBodyAndKeys(requestType, responseType);

        var routeHandlerBuilder = app.MapPost(pattern, requestDelegate)
            .WithTags(postAttribute.Tag)
            .Produces(StatusCodes.Status201Created, responseType);

        if (!string.IsNullOrWhiteSpace(postAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(postAttribute.Version);
        }
    }

    private static void MapRequestPost(this IEndpointRouteBuilder app, Type requestType)
    {
        var postAttribute = requestType.GetCustomAttribute<PostAttribute>()!;
        var pattern = Helpers.GetBasePattern(postAttribute);
        var responseType = Helpers.GetResponseType(requestType);

        var methodInfo = typeof(BuildEndpointsMethods)
            .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBody),
                BindingFlags.Static | BindingFlags.NonPublic)!
            .MakeGenericMethod(requestType, responseType);

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;

        var routeHandlerBuilder = app.MapPost(pattern, requestDelegate)
            .WithTags(postAttribute.Tag)
            .Produces(StatusCodes.Status200OK, responseType);

        if (!string.IsNullOrWhiteSpace(postAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(postAttribute.Version);
        }
    }

    private static void MapRequestPostByKey(this IEndpointRouteBuilder app, Type requestType)
    {
        var postAttribute = requestType.GetCustomAttribute<PostByKeyAttribute>()!;
        var pattern = KeysRequestHelper.GetKeyPattern(postAttribute, requestType);
        var responseType = Helpers.GetResponseType(requestType);

        var requestDelegate = GetDelegateEndpointWithBodyAndKeys(requestType, responseType);

        var routeHandlerBuilder = app.MapPost(pattern, requestDelegate)
            .WithTags(postAttribute.Tag)
            .Produces(StatusCodes.Status201Created, responseType);

        if (!string.IsNullOrWhiteSpace(postAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(postAttribute.Version);
        }
    }

    private static void MapRequestDeleteByKey(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<DeleteByKeyAttribute>()!;
        var pattern = KeysRequestHelper.GetKeyPattern(createAttribute, requestType);
        var responseType = Helpers.GetResponseType(requestType);

        var requestDelegate = GetDelegateEndpointWithBodyAndKeys(requestType, responseType);

        var routeHandlerBuilder = app.MapDelete(pattern, requestDelegate)
            .WithTags(createAttribute.Tag)
            .Produces(StatusCodes.Status200OK);

        if (!string.IsNullOrWhiteSpace(createAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(createAttribute.Version);
        }
    }

    private static void MapRequestDelete(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<DeleteAttribute>()!;
        var pattern = Helpers.GetBasePattern(createAttribute);
        var responseType = Helpers.GetResponseType(requestType);

        var methodInfo = typeof(BuildEndpointsMethods)
            .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBody),
                BindingFlags.Static | BindingFlags.NonPublic)!
            .MakeGenericMethod(requestType, responseType);

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;

        var routeHandlerBuilder = app.MapDelete(pattern, requestDelegate)
            .WithTags(createAttribute.Tag)
            .Produces(StatusCodes.Status200OK);

        if (!string.IsNullOrWhiteSpace(createAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(createAttribute.Version);
        }
    }

    private static void MapRequestGetByKey(this IEndpointRouteBuilder app, Type requestType)
    {
        var arrt = requestType.GetCustomAttribute<GetByKeyAttribute>();
        var pattern = KeysRequestHelper.GetKeyPattern(arrt, requestType);
        var responseType = Helpers.GetResponseType(requestType);

        var requestDelegate = GetDelegateEndpointWithGetParamsAndKeys(requestType, responseType);

        var routeHandlerBuilder = app.MapGet(pattern, requestDelegate)
            .WithTags(arrt.Tag)
            .Produces(StatusCodes.Status200OK, responseType);

        if (!string.IsNullOrWhiteSpace(arrt.Version))
        {
            routeHandlerBuilder.WithGroupName(arrt.Version);
        }
    }

    private static void MapRequestGet(this IEndpointRouteBuilder app, Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<GetAttribute>()!;
        var pattern = Helpers.GetBasePattern(attribute);
        var responseType = Helpers.GetResponseType(requestType);

        var methodInfo = typeof(BuildEndpointsMethods)
            .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithGetParams),
                BindingFlags.Static | BindingFlags.NonPublic)!
            .MakeGenericMethod(requestType, responseType);

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;

        var routeHandlerBuilder = app.MapGet(pattern, requestDelegate)
            .WithTags(attribute.Tag)
            .Produces(StatusCodes.Status200OK, responseType);

        if (!string.IsNullOrWhiteSpace(attribute.Version))
        {
            routeHandlerBuilder.WithGroupName(attribute.Version);
        }
    }

    internal static Delegate GetDelegateEndpointWithGetParamsAndKeys(Type requestType, Type responseType)
    {
        MethodInfo methodInfo = null;
        var keysCount = KeysRequestHelper.GetKeysCount(requestType);
        switch (keysCount)
        {
            case 1:
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithGetParamsAnd1KeyEndpoint),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, KeysRequestHelper.GetKeyType(requestType));
                break;
            case 2:
                var keys2 = KeysRequestHelper.GetKey2Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithGetParamsAnd2KeysEndpoint),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys2.Item1, keys2.Item2);
                break;
            case 3:
                var keys3 = KeysRequestHelper.GetKey3Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithGetParamsAnd3KeysEndpoint),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys3.Item1, keys3.Item2, keys3.Item3);
                break;
            case 4:
                var keys4 = KeysRequestHelper.GetKey4Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithGetParamsAnd4KeysEndpoint),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys4.Item1, keys4.Item2, keys4.Item3, keys4.Item4);
                break;
            case 5:
                var keys5 = KeysRequestHelper.GetKey5Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithGetParamsAnd5KeysEndpoint),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys5.Item1, keys5.Item2, keys5.Item3, keys5.Item4,
                        keys5.Item5);
                break;
            case 6:
                var keys6 = KeysRequestHelper.GetKey6Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithGetParamsAnd6KeysEndpoint),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys6.Item1, keys6.Item2, keys6.Item3, keys6.Item4,
                        keys6.Item5, keys6.Item6);
                break;
            case 7:
                var keys7 = KeysRequestHelper.GetKey7Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithGetParamsAnd7KeysEndpoint),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys7.Item1, keys7.Item2, keys7.Item3, keys7.Item4,
                        keys7.Item5, keys7.Item6, keys7.Item7);
                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;
        return requestDelegate;
    }

    internal static Delegate GetDelegateEndpointWithBodyAndKeys(Type requestType, Type responseType)
    {
        MethodInfo methodInfo = null;
        var keysCount = KeysRequestHelper.GetKeysCount(requestType);
        switch (keysCount)
        {
            case 1:
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBodyAnd1Key),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, KeysRequestHelper.GetKeyType(requestType));
                break;
            case 2:
                var keys2 = KeysRequestHelper.GetKey2Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBodyAnd2Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys2.Item1, keys2.Item2);
                break;
            case 3:
                var keys3 = KeysRequestHelper.GetKey3Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBodyAnd3Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys3.Item1, keys3.Item2, keys3.Item3);
                break;
            case 4:
                var keys4 = KeysRequestHelper.GetKey4Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBodyAnd4Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys4.Item1, keys4.Item2, keys4.Item3, keys4.Item4);
                break;
            case 5:
                var keys5 = KeysRequestHelper.GetKey5Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBodyAnd5Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys5.Item1, keys5.Item2, keys5.Item3, keys5.Item4,
                        keys5.Item5);
                break;
            case 6:
                var keys6 = KeysRequestHelper.GetKey6Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBodyAnd6Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys6.Item1, keys6.Item2, keys6.Item3, keys6.Item4,
                        keys6.Item5, keys6.Item6);
                break;
            case 7:
                var keys7 = KeysRequestHelper.GetKey7Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildEndpointWithBodyAnd7Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys7.Item1, keys7.Item2, keys7.Item3, keys7.Item4,
                        keys7.Item5, keys7.Item6, keys7.Item7);
                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;
        return requestDelegate;
    }

    internal static Delegate GetCreateDelegateEndpointWithBodyAndKeys(Type requestType, Type responseType)
    {
        MethodInfo methodInfo = null;
        var keysCount = KeysRequestHelper.GetKeysCount(requestType);
        switch (keysCount)
        {
            case 1:
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildCreateEndpointWithBodyAnd1Key),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, KeysRequestHelper.GetKeyType(requestType));
                break;
            case 2:
                var keys2 = KeysRequestHelper.GetKey2Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildCreateEndpointWithBodyAnd2Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys2.Item1, keys2.Item2);
                break;
            case 3:
                var keys3 = KeysRequestHelper.GetKey3Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildCreateEndpointWithBodyAnd3Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys3.Item1, keys3.Item2, keys3.Item3);
                break;
            case 4:
                var keys4 = KeysRequestHelper.GetKey4Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildCreateEndpointWithBodyAnd4Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys4.Item1, keys4.Item2, keys4.Item3, keys4.Item4);
                break;
            case 5:
                var keys5 = KeysRequestHelper.GetKey5Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildCreateEndpointWithBodyAnd5Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys5.Item1, keys5.Item2, keys5.Item3, keys5.Item4,
                        keys5.Item5);
                break;
            case 6:
                var keys6 = KeysRequestHelper.GetKey6Type(requestType);
                methodInfo = typeof(BuildEndpointsMethods)
                    .GetMethod(nameof(BuildEndpointsMethods.BuildCreateEndpointWithBodyAnd6Keys),
                        BindingFlags.Static | BindingFlags.NonPublic)!
                    .MakeGenericMethod(requestType, responseType, keys6.Item1, keys6.Item2, keys6.Item3, keys6.Item4,
                        keys6.Item5, keys6.Item6);
                break;
        }

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;
        return requestDelegate;
    }
}