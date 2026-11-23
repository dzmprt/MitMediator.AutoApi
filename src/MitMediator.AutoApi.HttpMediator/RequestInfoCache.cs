using System.Collections.Concurrent;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.HttpMediator;

internal static class RequestInfoCache
{
    private static readonly ConcurrentDictionary<(Type Type, string BasePath), RequestInfo> Cache = new();

    public static RequestInfo Get(Type requestType, string? basePath = null)
    {
        return Cache.GetOrAdd(
            (requestType, basePath ?? string.Empty),
            key => new RequestInfo(key.Type, key.BasePath.Length == 0 ? null : key.BasePath));
    }
}
