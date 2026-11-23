using System.Collections.Frozen;
using System.Collections.Concurrent;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

internal static class KeyRequestProperties
{
    private static readonly ConcurrentDictionary<Type, IReadOnlySet<string>> Names = new();

    public static IReadOnlySet<string> GetNames(Type requestType)
    {
        return Names.GetOrAdd(requestType, GetNamesCore);
    }

    private static IReadOnlySet<string> GetNamesCore(Type requestType)
    {
        if (!RequestInfo.GetIsKeyRequest(requestType))
        {
            return new HashSet<string>();
        }

        var keyInterface = requestType.GetInterfaces()
            .First(interfaceType => interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition().Name.StartsWith("IKeyRequest"));

        return keyInterface.GetProperties()
            .Select(property => property.Name)
            .ToFrozenSet(StringComparer.Ordinal);
    }
}