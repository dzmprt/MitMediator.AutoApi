using System.Globalization;
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
                    url = url.Replace($"{{key{i + 1}}}", ToRouteString(patternKeys[i]));
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
        if (!RequestInfo.GetIsKeyRequest(type))
            return Array.Empty<object>();

        var keyInterface = type.GetInterfaces()
            .First(interfaceType => interfaceType.IsGenericType &&
                interfaceType.GetGenericTypeDefinition().Name.StartsWith("IKeyRequest"));
        var keysCount = keyInterface.GetGenericArguments().Length;

        return Enumerable.Range(0, keysCount)
            .Select(index => keyInterface.GetProperty(index == 0 && keysCount == 1 ? "Key" : $"Key{index + 1}"))
            .Select(property => property!.GetValue(obj))
            .ToArray()!;
    }

    public static string ToRouteString(object? value)
    {
        if (value == null)
            return "null";

        return value switch
        {
            int i => i.ToString(CultureInfo.InvariantCulture),
            long l => l.ToString(CultureInfo.InvariantCulture),
            float f => f.ToString(CultureInfo.InvariantCulture),
            double d => d.ToString(CultureInfo.InvariantCulture),
            decimal m => m.ToString(CultureInfo.InvariantCulture),
            short s => s.ToString(CultureInfo.InvariantCulture),
            byte b => b.ToString(CultureInfo.InvariantCulture),
            DateTime dt => dt.ToString("O", CultureInfo.InvariantCulture), // ISO 8601 (round-trip)
            DateTimeOffset dto => dto.ToString("O", CultureInfo.InvariantCulture),
            _ => value?.ToString() ?? "null"
        };
    }
}