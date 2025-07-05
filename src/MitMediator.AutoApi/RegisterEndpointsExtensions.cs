using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

public static class RegisterEndpointsExtensions
{
    public static IEndpointRouteBuilder UseAutoApi(this IEndpointRouteBuilder app)
    {
        var createRequests = GetTypesWithAttribute<CreateAttribute>();
        foreach (var request in createRequests)
        {
            app.MapRequestCreate(request);
        }
        
        var postRequests = GetTypesWithAttribute<PostAttribute>();
        foreach (var request in postRequests)
        {
            app.MapRequestPost(request);
        }
        
        var updateByKeyRequests = GetTypesWithAttribute<UpdateByKeyAttribute>();
        foreach (var request in updateByKeyRequests)
        {
            app.MapRequestUpdateByKey(request);
        }
        
        var updateRequests = GetTypesWithAttribute<UpdateAttribute>();
        foreach (var request in updateRequests)
        {
            app.MapRequestUpdate(request);
        }
        
        var deleteByKeyRequests = GetTypesWithAttribute<DeleteByKeyAttribute>();
        foreach (var request in deleteByKeyRequests)
        {
            app.MapRequestDeleteByKey(request);
        }
        
        var deleteRequests = GetTypesWithAttribute<DeleteAttribute>();
        foreach (var request in deleteRequests)
        {
            app.MapRequestDelete(request);
        }
        
        var getByKeyRequests = GetTypesWithAttribute<GetByKeyAttribute>();
        foreach (var getByKeyRequest in getByKeyRequests)
        {
            app.MapRequestGetByKey(getByKeyRequest);
        }
        
        var getRequests = GetTypesWithAttribute<GetAttribute>();
        foreach (var getRequest in getRequests)
        {
            app.MapRequestGet(getRequest);
        }

        return app;
    }

    #region UpdateByKey

    private static void MapRequestUpdateByKey(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<UpdateByKeyAttribute>()!;
        var pattern = GetKeyPattern(createAttribute);
        var responseType = GetResponseType(requestType);
        
        var methodInfo = typeof(RegisterEndpointsExtensions)
            .GetMethod(nameof(BuildPutByKeyEndpoint), BindingFlags.Static | BindingFlags.NonPublic)!
            .MakeGenericMethod(requestType, responseType, GetKeyType(requestType));

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;

        var routeHandlerBuilder = app.MapPut(pattern, requestDelegate)
            .WithTags(createAttribute.Tag)
            .Produces(StatusCodes.Status200OK, responseType);
        
        if (!string.IsNullOrWhiteSpace(createAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(createAttribute.Version);
        }
    }
    
    private static Delegate BuildPutByKeyEndpoint<TRequest, TResponse, TKey>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey>
    {
        return (Func<TRequest, TKey, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey(key);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    #endregion
    
    #region Update

    private static void MapRequestUpdate(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<UpdateAttribute>()!;
        var pattern = GetBasePattern(createAttribute);
        var responseType = GetResponseType(requestType);
        
        var methodInfo = typeof(RegisterEndpointsExtensions)
            .GetMethod(nameof(BuildPutEndpoint), BindingFlags.Static | BindingFlags.NonPublic)!
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
    
    private static Delegate BuildPutEndpoint<TRequest, TResponse>()
        where TRequest : IRequest<TResponse>
    {
        return (Func<TRequest,  HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    #endregion

    #region Create

    private static void MapRequestCreate(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<CreateAttribute>()!;
        var pattern = GetBasePattern(createAttribute);
        var responseType = GetResponseType(requestType);
        
        var methodInfo = typeof(RegisterEndpointsExtensions)
            .GetMethod(nameof(BuildPostCreateEndpoint), BindingFlags.Static | BindingFlags.NonPublic)!
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
    
    private static Delegate BuildPostCreateEndpoint<TRequest, TResponse>()
        where TRequest : IRequest<TResponse>
    {
        return (Func<TRequest, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                var keyPattern = GetKeyPattern(typeof(TRequest).GetCustomAttribute<CreateAttribute>()!);
                return Results.Created(keyPattern, result);
            });
    }

    #endregion
    
    #region Create

    private static void MapRequestPost(this IEndpointRouteBuilder app, Type requestType)
    {
        var postAttribute = requestType.GetCustomAttribute<PostAttribute>()!;
        var pattern = GetBasePattern(postAttribute);
        var responseType = GetResponseType(requestType);
        
        var methodInfo = typeof(RegisterEndpointsExtensions)
            .GetMethod(nameof(BuildPostEndpoint), BindingFlags.Static | BindingFlags.NonPublic)!
            .MakeGenericMethod(requestType, responseType);

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;

        var routeHandlerBuilder = app.MapPost(pattern, requestDelegate)
            .WithTags(postAttribute.Tag)
            .Produces(StatusCodes.Status201Created, responseType);
        
        if (!string.IsNullOrWhiteSpace(postAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(postAttribute.Version);
        }
    }
    
    private static Delegate BuildPostEndpoint<TRequest, TResponse>()
        where TRequest : IRequest<TResponse>
    {
        return (Func<TRequest, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    #endregion
    
    #region DeleteByKey

    private static void MapRequestDeleteByKey(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<DeleteByKeyAttribute>()!;
        var pattern = GetKeyPattern(createAttribute);
        var responseType = GetResponseType(requestType);
        
        var methodInfo = typeof(RegisterEndpointsExtensions)
            .GetMethod(nameof(BuildDeleteByKeyEndpoint), BindingFlags.Static | BindingFlags.NonPublic)!
            .MakeGenericMethod(requestType, responseType, GetKeyType(requestType));

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;

        var routeHandlerBuilder = app.MapDelete(pattern, requestDelegate)
            .WithTags(createAttribute.Tag)
            .Produces(StatusCodes.Status200OK);
        
        if (!string.IsNullOrWhiteSpace(createAttribute.Version))
        {
            routeHandlerBuilder.WithGroupName(createAttribute.Version);
        }
    }
    
    private static Delegate BuildDeleteByKeyEndpoint<TRequest, TResponse, TKey>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey>
    {
        return (Func<TRequest, TKey, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey(key);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    #endregion
    
    #region Delete

    private static void MapRequestDelete(this IEndpointRouteBuilder app, Type requestType)
    {
        var createAttribute = requestType.GetCustomAttribute<DeleteAttribute>()!;
        var pattern = GetBasePattern(createAttribute);
        var responseType = GetResponseType(requestType);
        
        var methodInfo = typeof(RegisterEndpointsExtensions)
            .GetMethod(nameof(BuildDeleteEndpoint), BindingFlags.Static | BindingFlags.NonPublic)!
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
    
    private static Delegate BuildDeleteEndpoint<TRequest, TResponse>()
        where TRequest : IRequest<TResponse>
    {
        return (Func<TRequest, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    #endregion

    #region GetByKey

    private static void MapRequestGetByKey(this IEndpointRouteBuilder app, Type requestType)
    {
        var arrt = requestType.GetCustomAttribute<GetByKeyAttribute>();
        var pattern = GetKeyPattern(arrt);
        var responseType = GetResponseType(requestType);
        
        var methodInfo = typeof(RegisterEndpointsExtensions)
            .GetMethod(nameof(BuildGetByKeyEndpoint), BindingFlags.Static | BindingFlags.NonPublic)!
            .MakeGenericMethod(requestType, responseType, GetKeyType(requestType));

        var requestDelegate = (Delegate)methodInfo.Invoke(null, Array.Empty<object>())!;

        var routeHandlerBuilder = app.MapGet(pattern, requestDelegate)
            .WithTags(arrt.Tag)
            .Produces(StatusCodes.Status200OK, responseType);
        
        if (!string.IsNullOrWhiteSpace(arrt.Version))
        {
            routeHandlerBuilder.WithGroupName(arrt.Version);
        }
    }

    private static Delegate BuildGetByKeyEndpoint<TRequest, TResponse, TKey>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey>
    {
        return (Func<TRequest, TKey, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([AsParameters] request, [FromRoute] key, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey(key);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    #endregion
    
    #region Get

    private static void MapRequestGet(this IEndpointRouteBuilder app, Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<GetAttribute>()!;
        var pattern = GetBasePattern(attribute);
        var responseType = GetResponseType(requestType);
        
        var methodInfo = typeof(RegisterEndpointsExtensions)
            .GetMethod(nameof(BuildGetEndpoint), BindingFlags.Static | BindingFlags.NonPublic)!
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

    private static Delegate BuildGetEndpoint<TRequest, TResponse>()
        where TRequest : IRequest<TResponse>
    {
        return (Func<TRequest, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([AsParameters] request, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    #endregion

    private static Type GetKeyType(Type queryType)
    {
        var requestKeyInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IKeyRequest<>));

        if (requestKeyInterface == null)
        {
            throw new Exception($"{queryType.Name} must implement IKeyRequest<> interface.");
        }

        return requestKeyInterface!.GetGenericArguments()[0];
    }

    private static Type GetResponseType(Type queryType)
    {
        var requestInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IRequest<>));

        return requestInterface!.GetGenericArguments()[0];
    }

    private static IEnumerable<Type> GetTypesWithAttribute<TAttribute>()
        where TAttribute : Attribute
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly =>
            {
                try
                {
                    return assembly.GetTypes();
                }
                catch (ReflectionTypeLoadException ex)
                {
                    return ex.Types.Where(t => t != null)!;
                }
            })
            .Where(type => type.GetCustomAttributes(typeof(TAttribute), inherit: true).Any());
    }

    private static string GetKeyPattern(BaseActionAttribute arrt)
    {
        if (arrt.CustomPattern is not null)
        {
            if (!arrt.CustomPattern.Contains("{key}"))
            {
                throw new Exception("Custom pattern must contain '{key}'.");
            }
            return arrt.CustomPattern;
        }
        var pattern = GetBasePattern(arrt);
        return string.Concat(pattern, "/{key}");
    }

    private static string GetBasePattern(BaseActionAttribute arrt)
    {
        if (arrt.CustomPattern is not null)
        {
            return arrt.CustomPattern;
        }

        var tag = arrt.Tag.ToLower();
        var version = arrt.Version;
        return !string.IsNullOrWhiteSpace(arrt.Version) ? string.Concat(version, "/", tag) : tag;
    }
}