using System.Reflection;

namespace MitMediator.AutoApi.Abstractions;

public static class RequestHelper
{
    public static IEnumerable<RequestInfo> GetRequestsInfos(Assembly[] assemblies, string? baseUrl = null)
    {
        var interfaceType = typeof(IRequest<>);
        var types =  assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                        && t is { IsAbstract: false, IsPublic: true });
        
        return types.Select(type => new RequestInfo(type, baseUrl)).ToList();
    }
}