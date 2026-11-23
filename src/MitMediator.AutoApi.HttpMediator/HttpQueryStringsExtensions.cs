using System.Collections;
using System.Collections.Concurrent;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text;

namespace MitMediator.AutoApi.HttpMediator;

public static class HttpQueryStringsExtensions
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> Properties = new();

    public static string ToQueryString(this object obj)
    {
        var sb = new StringBuilder();
        BuildQuery(obj, "", sb);
        return sb.Length == 0 ? string.Empty : $"?{sb.ToString().TrimStart('&')}";
    }

    private static void BuildQuery(object? obj, string prefix, StringBuilder sb)
    {
        if (obj == null) return;

        var type = obj.GetType();

        if (IsSimple(type))
        {
            if (IsDefault(obj)) return;
            var value = ConvertToString(obj);
            if (value != null)
                sb.Append('&').Append(WebUtility.UrlEncode(prefix)).Append('=').Append(WebUtility.UrlEncode(value));
            return;
        }

        if (obj is IEnumerable enumerable && !(obj is string))
        {
            foreach (var item in enumerable)
            {
                var itemPrefix = $"{prefix}";
                BuildQuery(item, itemPrefix, sb);
            }
            return;
        }

        if (IsDefault(obj)) return;

        var properties = Properties.GetOrAdd(
            type,
            currentType => currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance));
        foreach (var prop in properties)
        {
            if (!prop.CanRead) continue;
            if (prop.Name == "Key" || (prop.Name.StartsWith("Key") &&
                int.TryParse(prop.Name[3..], out _))) continue;

            var value = prop.GetValue(obj);
            var name = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";
            BuildQuery(value, name, sb);
        }
    }

    private static bool IsSimple(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        return type.IsPrimitive
            || type.IsEnum
            || type == typeof(string)
            || type == typeof(Guid)
            || type == typeof(DateTime)
            || type == typeof(DateTimeOffset)
            || type == typeof(decimal);
    }

    private static string? ConvertToString(object obj)
    {
        if (obj is DateTime dt)
            return dt.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
        if (obj is DateTimeOffset dto)
            return dto.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
        if (obj is bool b)
            return b ? "true" : "false";
        return Convert.ToString(obj, CultureInfo.InvariantCulture);
    }
    
    private static bool IsDefault(object? obj)
    {
        if (obj == null) return true;

        var type = obj.GetType();
        var underlying = Nullable.GetUnderlyingType(type) ?? type;

        if (underlying.IsValueType)
        {
            var defaultValue = Activator.CreateInstance(underlying);
            return obj.Equals(defaultValue);
        }

        if (obj is string s) return string.IsNullOrWhiteSpace(s);

        return false;
    }
}