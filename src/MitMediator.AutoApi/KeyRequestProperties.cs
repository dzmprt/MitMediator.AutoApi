using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

internal static class KeyRequestProperties
{
    public static HashSet<string> GetNames(Type requestType)
    {
        if (!RequestInfo.GetIsKeyRequest(requestType))
        {
            return [];
        }

        var keyInterface = requestType.GetInterfaces()
            .First(interfaceType => interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition().Name.StartsWith("IKeyRequest"));

        return keyInterface.GetProperties().Select(property => property.Name).ToHashSet();
    }
}