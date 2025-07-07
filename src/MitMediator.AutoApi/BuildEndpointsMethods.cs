using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

internal static class BuildEndpointsMethods
{
    internal static Delegate BuildEndpointWithBody<TRequest, TResponse>()
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

    #region Methods with body and keys

     internal static Delegate BuildEndpointWithBodyAnd1Key<TRequest, TResponse, TKey>()
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
    
    internal static Delegate BuildEndpointWithBodyAnd2Keys<TRequest, TResponse, TKey1, TKey2>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2>
    {
        return (Func<TRequest, TKey1, TKey2, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }
    
    internal static Delegate BuildEndpointWithBodyAnd3Keys<TRequest, TResponse, TKey1, TKey2, TKey3>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }
    
    internal static Delegate BuildEndpointWithBodyAnd4Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }
    
    internal static Delegate BuildEndpointWithBodyAnd5Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, [FromRoute] key5, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }
    
    internal static Delegate BuildEndpointWithBodyAnd6Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, [FromRoute] key5, [FromRoute] key6, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                request.SetKey6(key6);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }
    
    internal static Delegate BuildEndpointWithBodyAnd7Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, [FromRoute] key5, [FromRoute] key6, [FromRoute] key7, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                request.SetKey6(key6);
                request.SetKey7(key7);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    #endregion
    
    internal static Delegate BuildEndpointWithGetParams<TRequest, TResponse>()
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

    #region Methods with get params and keys

     internal static Delegate BuildEndpointWithGetParamsAnd1KeyEndpoint<TRequest, TResponse, TKey>()
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

    internal static Delegate BuildEndpointWithGetParamsAnd2KeysEndpoint<TRequest, TResponse, TKey1, TKey2>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2>
    {
        return (Func<TRequest, TKey1, TKey2, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([AsParameters] request, [FromRoute] key1, [FromRoute] key2, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    internal static Delegate BuildEndpointWithGetParamsAnd3KeysEndpoint<TRequest, TResponse, TKey1, TKey2, TKey3>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([AsParameters] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    internal static Delegate BuildEndpointWithGetParamsAnd4KeysEndpoint<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([AsParameters] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, ctx,
                ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    internal static Delegate BuildEndpointWithGetParamsAnd5KeysEndpoint<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([AsParameters] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4,
                [FromRoute] key5, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    internal static Delegate BuildEndpointWithGetParamsAnd6KeysEndpoint<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, HttpContext, CancellationToken,
            ValueTask<IResult>>)(
            async ([AsParameters] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4,
                [FromRoute] key5, [FromRoute] key6, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                request.SetKey6(key6);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    internal static Delegate BuildEndpointWithGetParamsAnd7KeysEndpoint<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6,
        TKey7>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, HttpContext, CancellationToken,
            ValueTask<IResult>>)(
            async ([AsParameters] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4,
                [FromRoute] key5, [FromRoute] key6, [FromRoute] key7, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                request.SetKey6(key6);
                request.SetKey7(key7);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return result is Unit ? Results.Ok() : Results.Ok(result);
            });
    }

    #endregion
    
    internal static Delegate BuildCreateEndpoint<TRequest, TResponse>()
        where TRequest : IRequest<TResponse>
    {
        return (Func<TRequest, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                var keyPattern =
                    string.Concat(Helpers.GetBasePattern(typeof(TRequest).GetCustomAttribute<CreateAttribute>()!), "/{key}");
                return Results.Created(keyPattern, result);
            });
    }
    
    #region Methods with body and keys and 201 result

     internal static Delegate BuildCreateEndpointWithBodyAnd1Key<TRequest, TResponse, TKey>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey>
    {
        return (Func<TRequest, TKey, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey(key);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                var requestType = typeof(TRequest);
                var keyPattern =
                    KeysRequestHelper.GetKeyPattern(requestType.GetCustomAttribute<CreateByKeyAttribute>()!,
                        requestType);
                keyPattern = keyPattern.Replace("{key}", key?.ToString());
                keyPattern = string.Concat(keyPattern, "/{key}");
                return result is Unit ? Results.Created(keyPattern, null) : Results.Created(keyPattern, result);
            });
    }
    
    internal static Delegate BuildCreateEndpointWithBodyAnd2Keys<TRequest, TResponse, TKey1, TKey2>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2>
    {
        return (Func<TRequest, TKey1, TKey2, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                var requestType = typeof(TRequest);
                var keyPattern = string.Concat(KeysRequestHelper.GetKeyPattern(requestType.GetCustomAttribute<CreateByKeyAttribute>()!, requestType), "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                return result is Unit ? Results.Created(keyPattern, null) : Results.Created(keyPattern, result);
            });
    }
    
    internal static Delegate BuildCreateEndpointWithBodyAnd3Keys<TRequest, TResponse, TKey1, TKey2, TKey3>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                var requestType = typeof(TRequest);
                var keyPattern = string.Concat(KeysRequestHelper.GetKeyPattern(requestType.GetCustomAttribute<CreateByKeyAttribute>()!, requestType), "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                return result is Unit ? Results.Created(keyPattern, null) : Results.Created(keyPattern, result);
            });
    }
    
    internal static Delegate BuildCreateEndpointWithBodyAnd4Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                var requestType = typeof(TRequest);
                var keyPattern = string.Concat(KeysRequestHelper.GetKeyPattern(requestType.GetCustomAttribute<CreateByKeyAttribute>()!, requestType), "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                return result is Unit ? Results.Created(keyPattern, null) : Results.Created(keyPattern, result);
            });
    }
    
    internal static Delegate BuildCreateEndpointWithBodyAnd5Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, [FromRoute] key5, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                var requestType = typeof(TRequest);
                var keyPattern = string.Concat(KeysRequestHelper.GetKeyPattern(requestType.GetCustomAttribute<CreateByKeyAttribute>()!, requestType), "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                keyPattern = keyPattern.Replace("{key5}", key5?.ToString());
                return result is Unit ? Results.Created(keyPattern, null) : Results.Created(keyPattern, result);
            });
    }
    
    internal static Delegate BuildCreateEndpointWithBodyAnd6Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>()
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, [FromRoute] key5, [FromRoute] key6, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                request.SetKey6(key6);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                var requestType = typeof(TRequest);
                var keyPattern = string.Concat(KeysRequestHelper.GetKeyPattern(requestType.GetCustomAttribute<CreateByKeyAttribute>()!, requestType), "/{key");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                keyPattern = keyPattern.Replace("{key5}", key5?.ToString());
                keyPattern = keyPattern.Replace("{key6}", key6?.ToString());
                return result is Unit ? Results.Created(keyPattern, null) : Results.Created(keyPattern, result);
            });
    }

    #endregion
}