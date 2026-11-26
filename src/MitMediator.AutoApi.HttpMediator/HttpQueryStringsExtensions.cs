using System.Collections;
using System.Collections.Concurrent;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.Text;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.HttpMediator;

public static class HttpQueryStringsExtensions
{
    private static readonly ConcurrentDictionary<Type, PropertyInfo[]> Properties = new();

    public static string ToQueryString(this object obj)
    {
        var query = new StringBuilder();
        foreach (var field in GetFields(obj, "", asForm: false, keepDefault: false))
        {
            query.Append(query.Length == 0 ? '?' : '&')
                .Append(WebUtility.UrlEncode(field.Key))
                .Append('=')
                .Append(WebUtility.UrlEncode(field.Value));
        }

        return query.ToString();
    }

    internal static IEnumerable<KeyValuePair<string, string>> GetFormFields(this object obj) =>
        GetFields(obj, "", asForm: true, keepDefault: true);

    private static IEnumerable<KeyValuePair<string, string>> GetFields(
        object? obj, string prefix, bool asForm, bool keepDefault)
    {
        if (obj is null)
        {
            yield break;
        }

        var type = obj.GetType();
        if (IsSimple(type))
        {
            if ((keepDefault || !IsDefault(obj)) && ConvertToString(obj) is { } value)
            {
                yield return new(prefix, value);
            }

            yield break;
        }

        if (obj is IEnumerable items)
        {
            var index = 0;
            foreach (var item in items)
            {
                var name = asForm ? $"{prefix}[{index}]" : prefix;
                foreach (var field in GetFields(item, name, asForm, keepDefault: true))
                {
                    yield return field;
                }

                index++;
            }

            yield break;
        }

        var properties = Properties.GetOrAdd(type, static currentType =>
        {
            var keys = KeyRequestProperties.GetNames(currentType);
            return currentType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property => property.GetMethod?.IsPublic == true &&
                    property.GetIndexParameters().Length == 0 && !keys.Contains(property.Name))
                .ToArray();
        });

        foreach (var property in properties)
        {
            var name = string.IsNullOrEmpty(prefix) ? property.Name : $"{prefix}.{property.Name}";
            var keepPropertyDefault = asForm || Nullable.GetUnderlyingType(property.PropertyType) is not null;
            foreach (var field in GetFields(property.GetValue(obj), name, asForm, keepPropertyDefault))
            {
                yield return field;
            }
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