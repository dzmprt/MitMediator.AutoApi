using System.Collections.Concurrent;
using System.Collections.Frozen;

namespace MitMediator.AutoApi.Abstractions;

internal static class KeyRequestProperties
{
    private static readonly ConcurrentDictionary<Type, IReadOnlySet<string>> Names = new();

    public static IReadOnlySet<string> GetNames(Type requestType) =>
        Names.GetOrAdd(requestType, static type =>
            RequestInfo.GetKeyRequestInterface(type)?.GetProperties()
                .Select(property => property.Name)
                .ToFrozenSet(StringComparer.Ordinal) ?? FrozenSet<string>.Empty);
}
