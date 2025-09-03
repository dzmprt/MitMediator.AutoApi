using System.Collections;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

/// <summary>
/// Delegates for minimal api registrations.
/// </summary>
internal static class EndpointsMethods
{
    internal static Delegate WithGetParams<TRequest, TResponse>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>
    {
        return (Func<HttpContext, CancellationToken, ValueTask<IResult>>)(
            async (ctx, ct) =>
            {
                var request = QueryBinder.BindFromQuery<TRequest>(ctx);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return GetApiResult(requestInfo, result, ctx);
            });
    }

    #region Methods with get params and keys

    internal static Delegate WithGetParamsAnd1Key<TRequest, TResponse, TKey>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey>
    {
        return (Func<TKey, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromRoute] key, ctx, ct) =>
            {
                var request = QueryBinder.BindFromQuery<TRequest>(ctx);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey(key);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return GetApiResult(requestInfo, result, ctx);
            });
    }

    internal static Delegate WithGetParamsAnd2Keys<TRequest, TResponse, TKey1, TKey2>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2>
    {
        return (Func<TKey1, TKey2, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromRoute] key1, [FromRoute] key2, ctx, ct) =>
            {
                var request = QueryBinder.BindFromQuery<TRequest>(ctx);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return GetApiResult(requestInfo, result, ctx);
            });
    }

    internal static Delegate WithGetParamsAnd3Keys<TRequest, TResponse, TKey1, TKey2, TKey3>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3>
    {
        return (Func<TKey1, TKey2, TKey3, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromRoute] key1, [FromRoute] key2, [FromRoute] key3, ctx, ct) =>
            {
                var request = QueryBinder.BindFromQuery<TRequest>(ctx);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return GetApiResult(requestInfo, result, ctx);
            });
    }

    internal static Delegate WithGetParamsAnd4Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4>
    {
        return (Func<TKey1, TKey2, TKey3, TKey4, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, ctx,
                ct) =>
            {
                var request = QueryBinder.BindFromQuery<TRequest>(ctx);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return GetApiResult(requestInfo, result, ctx);
            });
    }

    internal static Delegate WithGetParamsAnd5Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        return (Func<TKey1, TKey2, TKey3, TKey4, TKey5, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4,
                [FromRoute] key5, ctx, ct) =>
            {
                var request = QueryBinder.BindFromQuery<TRequest>(ctx);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return GetApiResult(requestInfo, result, ctx);
            });
    }

    internal static Delegate WithGetParamsAnd6Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        return (Func<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, HttpContext, CancellationToken,
            ValueTask<IResult>>)(
            async ([FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4,
                [FromRoute] key5, [FromRoute] key6, ctx, ct) =>
            {
                var request = QueryBinder.BindFromQuery<TRequest>(ctx);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                request.SetKey6(key6);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return GetApiResult(requestInfo, result, ctx);
            });
    }

    internal static Delegate WithGetParamsAnd7Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6,
        TKey7>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
    {
        return (Func<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, HttpContext, CancellationToken,
            ValueTask<IResult>>)(
            async ([FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4,
                [FromRoute] key5, [FromRoute] key6, [FromRoute] key7, ctx, ct) =>
            {
                var request = QueryBinder.BindFromQuery<TRequest>(ctx);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                request.SetKey6(key6);
                request.SetKey7(key7);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                return GetApiResult(requestInfo, result, ctx);
            });
    }

    #endregion

    internal static Delegate FormWithFile<TRequest, TResponse>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IFileRequest
    {
        return (Func<IFormFile, TRequest?, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async (formFile, [FromForm] request, ctx, ct) =>
            {
                request ??= Activator.CreateInstance<TRequest>();
                request.SetFile(formFile.OpenReadStream(), formFile.FileName);

                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)
                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = $"{requestInfo.Pattern}/{{key}}";
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    internal static Delegate WithBody<TRequest, TResponse>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>
    {
        return (Func<TRequest, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();

                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)
                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = $"{requestInfo.Pattern}/{{key}}";
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    #region Methods with body/form and keys and 201 result

    internal static Delegate WithBodyAnd1Key<TRequest, TResponse, TKey>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey>
    {
        return (Func<TRequest, TKey, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey(key);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = requestInfo.Pattern;
                keyPattern = keyPattern.Replace("{key}", key?.ToString());
                keyPattern = string.Concat(keyPattern, "/{key}");
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    internal static Delegate FormWithFileAnd1Key<TRequest, TResponse, TKey>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IFileRequest, IKeyRequest<TKey>
    {
        return (Func<IFormFile, TRequest?, TKey, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async (formFile, [FromForm] request, [FromRoute] key, ctx, ct) =>
            {
                request ??= Activator.CreateInstance<TRequest>();
                request.SetFile(formFile.OpenReadStream(), formFile.FileName);
                request.SetKey(key);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = $"{requestInfo.Pattern}/{{key}}";
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    internal static Delegate WithBodyAnd2Keys<TRequest, TResponse, TKey1, TKey2>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2>
    {
        return (Func<TRequest, TKey1, TKey2, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }
    
    internal static Delegate FormWithFileAnd2Key<TRequest, TResponse, TKey1, TKey2>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IFileRequest, IKeyRequest<TKey1, TKey2>
    {
        return (Func<IFormFile, TRequest?, TKey1, TKey2, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async (formFile, [FromForm] request, [FromRoute] key1, [FromRoute] key2, ctx, ct) =>
            {
                request ??= Activator.CreateInstance<TRequest>();
                request.SetFile(formFile.OpenReadStream(), formFile.FileName);
                request.SetKey1(key1);
                request.SetKey2(key2);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }
                
                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    internal static Delegate WithBodyAnd3Keys<TRequest, TResponse, TKey1, TKey2, TKey3>(RequestInfo requestInfo)
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
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }
    
    internal static Delegate FormWithFileAnd3Key<TRequest, TResponse, TKey1, TKey2, TKey3>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IFileRequest, IKeyRequest<TKey1, TKey2, TKey3>
    {
        return (Func<IFormFile, TRequest?, TKey1, TKey2, TKey3, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async (formFile, [FromForm] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, ctx, ct) =>
            {
                request ??= Activator.CreateInstance<TRequest>();
                request.SetFile(formFile.OpenReadStream(), formFile.FileName);
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }
                
                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    internal static Delegate WithBodyAnd4Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, ctx,
                ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }
    
    internal static Delegate FormWithFileAnd4Key<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IFileRequest, IKeyRequest<TKey1, TKey2, TKey3, TKey4>
    {
        return (Func<IFormFile, TRequest?, TKey1, TKey2, TKey3, TKey4, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async (formFile, [FromForm] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, ctx, ct) =>
            {
                request ??= Activator.CreateInstance<TRequest>();
                request.SetFile(formFile.OpenReadStream(), formFile.FileName);
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }
                
                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    internal static Delegate WithBodyAnd5Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4,
                [FromRoute] key5, ctx, ct) =>
            {
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                keyPattern = keyPattern.Replace("{key5}", key5?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }
    
    internal static Delegate FormWithFileAnd5Key<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IFileRequest, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        return (Func<IFormFile, TRequest?, TKey1, TKey2, TKey3, TKey4, TKey5, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async (formFile, [FromForm] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, [FromRoute] key5, ctx, ct) =>
            {
                request ??= Activator.CreateInstance<TRequest>();
                request.SetFile(formFile.OpenReadStream(), formFile.FileName);
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }
                
                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                keyPattern = keyPattern.Replace("{key5}", key5?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    internal static Delegate WithBodyAnd6Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, HttpContext, CancellationToken,
            ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4,
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
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = string.Concat(requestInfo.Pattern, "/{key");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                keyPattern = keyPattern.Replace("{key5}", key5?.ToString());
                keyPattern = keyPattern.Replace("{key6}", key6?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }
    
    internal static Delegate FormWithFileAnd6Key<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IFileRequest, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        return (Func<IFormFile, TRequest?, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async (formFile, [FromForm] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, [FromRoute] key5, [FromRoute] key6, ctx, ct) =>
            {
                request ??= Activator.CreateInstance<TRequest>();
                request.SetFile(formFile.OpenReadStream(), formFile.FileName);
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                request.SetKey6(key6);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }
                
                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                keyPattern = keyPattern.Replace("{key5}", key5?.ToString());
                keyPattern = keyPattern.Replace("{key6}", key6?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    internal static Delegate WithBodyAnd7Keys<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
    {
        return (Func<TRequest, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, HttpContext, CancellationToken,
            ValueTask<IResult>>)(
            async ([FromBody] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4,
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
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }

                var keyPattern = string.Concat(requestInfo.Pattern, "/{key");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                keyPattern = keyPattern.Replace("{key5}", key5?.ToString());
                keyPattern = keyPattern.Replace("{key6}", key6?.ToString());
                keyPattern = keyPattern.Replace("{key7}", key7?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }
    
        internal static Delegate FormWithFileAnd7Key<TRequest, TResponse, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>(RequestInfo requestInfo)
        where TRequest : IRequest<TResponse>, IFileRequest, IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
    {
        return (Func<IFormFile, TRequest?, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, HttpContext, CancellationToken, ValueTask<IResult>>)(
            async (formFile, [FromForm] request, [FromRoute] key1, [FromRoute] key2, [FromRoute] key3, [FromRoute] key4, [FromRoute] key5, [FromRoute] key6, [FromRoute] key7, ctx, ct) =>
            {
                request ??= Activator.CreateInstance<TRequest>();
                request.SetFile(formFile.OpenReadStream(), formFile.FileName);
                request.SetKey1(key1);
                request.SetKey2(key2);
                request.SetKey3(key3);
                request.SetKey4(key4);
                request.SetKey5(key5);
                request.SetKey6(key6);
                request.SetKey7(key7);
                var mediator = ctx.RequestServices.GetRequiredService<IMediator>();
                var result = await mediator.SendAsync<TRequest, TResponse>(request, ct);
                if (requestInfo.MethodType != MethodType.PostCreate)                {
                    return GetApiResult(requestInfo, result, ctx);
                }
                
                var keyPattern = string.Concat(requestInfo.Pattern, "/{key}");
                keyPattern = keyPattern.Replace("{key1}", key1?.ToString());
                keyPattern = keyPattern.Replace("{key2}", key2?.ToString());
                keyPattern = keyPattern.Replace("{key3}", key3?.ToString());
                keyPattern = keyPattern.Replace("{key4}", key4?.ToString());
                keyPattern = keyPattern.Replace("{key5}", key5?.ToString());
                keyPattern = keyPattern.Replace("{key6}", key6?.ToString());
                keyPattern = keyPattern.Replace("{key7}", key7?.ToString());
                return GetApiResult(requestInfo, result, ctx, keyPattern);
            });
    }

    #endregion

    private static IResult GetApiResult<TResponse>(
        RequestInfo requestInfo,
        TResponse result, 
        HttpContext ctx,
        string? resourceUrl = null)
    {
        if (!string.IsNullOrWhiteSpace(resourceUrl))
        {
            if (result is IResourceKey resourceKey)
            {
                resourceUrl = resourceUrl.Replace("{key}", resourceKey.GetResourceKey());
            }
        }

        if (result is ITotalCount totalCount)
        {
            ctx.Response.Headers.Append("X-Total-Count", totalCount.GetTotalCount().ToString());
        }

        switch (result)
        {
            case Unit:
                return resourceUrl is null ? Results.Ok() : Results.Created(resourceUrl, null);
            case byte[] bytes:
                return Results.File(bytes, requestInfo.ContentType ?? "application/octet-stream");
            case FileResponse fileResponse:
                return Results.File(fileResponse.File, requestInfo.ContentType ?? "application/octet-stream", fileResponse.FileName);
            case Stream stream:
                stream.Seek(0, SeekOrigin.Begin);
                return Results.File(stream, requestInfo.ContentType ?? "application/octet-stream");
            case FileStreamResponse fileStreamResponse:
                fileStreamResponse.File.Seek(0, SeekOrigin.Begin);
                return Results.File(fileStreamResponse.File, requestInfo.ContentType ?? "application/octet-stream", fileStreamResponse.FileName);
        }

        return resourceUrl is null ? Results.Ok(result) : Results.Created(resourceUrl, result);
    }
}