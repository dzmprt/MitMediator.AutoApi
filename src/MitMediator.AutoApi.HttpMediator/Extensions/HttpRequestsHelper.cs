using System.Collections.Concurrent;
using System.Globalization;
using System.Reflection;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.HttpMediator.Extensions;

internal static class HttpRequestsHelper
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> KeyProperties = new();

    public static string GetUrl<TRequest>(TRequest request, string? baseUrl = null)
    {
        var requestType = typeof(TRequest);
        var requestInfo = RequestInfoCache.Get(requestType, baseUrl);
        var url = requestInfo.Pattern;
        var patternKeys = ExtractKeys(request);
        if (patternKeys.Any())
        {
            if (patternKeys.Length == 1)
            {
                url = url.Replace("{key}", ToRouteString(patternKeys[0]));
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

        var keyProperties = KeyProperties.GetOrAdd(type, CreateKeyProperties);
        var values = new object[keyProperties.Length];
        for (var index = 0; index < keyProperties.Length; index++)
        {
            values[index] = keyProperties[index].GetValue(obj)!;
        }

        return values;
    }

    private static PropertyInfo[] CreateKeyProperties(Type type)
    {
        var keyInterface = RequestInfo.GetKeyRequestInterface(type)!;
        var keysCount = keyInterface.GetGenericArguments().Length;
        var properties = new PropertyInfo[keysCount];
        for (var index = 0; index < keysCount; index++)
        {
            var propertyName = index == 0 && keysCount == 1 ? "Key" : $"Key{index + 1}";
            properties[index] = keyInterface.GetProperty(propertyName)!;
        }

        return properties;
    }

    public static string ToRouteString(object? value)
    {
        if (value == null)
            return "null";

        var routeValue = value switch
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

        return Uri.EscapeDataString(routeValue);
    }
}