using System.Reflection;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.HttpMediator.Extensions;

internal static class HttpRequestsHelper
{
    public static string GetUrl<TRequest>(TRequest request, string? baseUrl = null)
    {
        var requestType = typeof(TRequest);
        var requestInfo = new RequestInfo(requestType, baseUrl);
        var url = requestInfo.Pattern;
        var patternKeys = ExtractKeys(request);
        if (patternKeys.Any())
        {
            if (patternKeys.Length == 1)
            {
                url = url.Replace("{key}", patternKeys[0]?.ToString() ?? "null");
            }
            else
            {
                for (var i = 0; i < patternKeys.Length; i++)
                {
                    url = url.Replace($"{{key{i+1}}}", patternKeys[i]?.ToString() ?? "null");
                }
            }
        }

        if (requestInfo.MethodType == MethodType.Delete || requestInfo.MethodType == MethodType.Get)
        {
            url += request.ToQueryString();
        }

        return url;
    }
    
    private static object[] ExtractKeys(object obj)
    {
        var type = obj.GetType();
        
        var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public)
            .Where(m => m.Name.StartsWith("GetKey"))
            .OrderBy(m => m.Name == "GetKey" ? 0 : int.TryParse(m.Name.Substring("GetKey".Length), out var index) ? index : 1000);

        return (from method in methods where method.GetParameters().Length == 0 select method.Invoke(obj, null)).ToArray();
    }
}