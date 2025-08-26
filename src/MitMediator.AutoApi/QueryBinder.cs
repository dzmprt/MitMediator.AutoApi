using System.Collections;
using System.Globalization;
using System.Reflection;

namespace MitMediator.AutoApi;

internal static class QueryBinder
{
    public static T BindFromQuery<T>(HttpContext context)
    {
        var query = context.Request.Query;
        var obj = (T)BindObject(typeof(T), "", query)!;
        return obj;
    }

    private static object? BindObject(Type type, string prefix, IQueryCollection query)
    {
        if (type == typeof(string) || type.IsPrimitive || type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(Guid))
            return ConvertSimple(query[prefix], type);

        if ((Nullable.GetUnderlyingType(type) ?? type).IsEnum)
            return Enum.Parse(Nullable.GetUnderlyingType(type) ?? type, query[prefix], ignoreCase: true);

        if (Nullable.GetUnderlyingType(type) is Type underlying)
            return string.IsNullOrEmpty(query[prefix]) ? null : ConvertSimple(query[prefix], underlying);

        if (type.IsArray)
        {
            var itemType = type.GetElementType()!;
            var values = query[prefix];
            var array = Array.CreateInstance(itemType, values.Count);
            for (var i = 0; i < values.Count; i++)
                array.SetValue(ConvertSimple(values[i], itemType), i);
            return array;
        }

        if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType)
        {
            var itemType = type.GetGenericArguments()[0];
            var values = query[prefix];
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(itemType))!;
            foreach (var val in values)
                list.Add(ConvertSimple(val, itemType));
            return list;
        }

        var obj = Activator.CreateInstance(type)!;
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanWrite) continue;

            var key = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";
            var propType = prop.PropertyType;

            if (query.ContainsKey(key) || query.Keys.Any(k => k.StartsWith(key + ".")))
            {
                var value = BindObject(propType, key, query);
                prop.SetValue(obj, value);
            }
        }

        return obj;
    }

    private static object? ConvertSimple(string value, Type type)
    {
        if (type == typeof(string)) return value;
        if (type == typeof(int)) return int.Parse(value);
        if (type == typeof(long)) return long.Parse(value);
        if (type == typeof(bool)) return bool.Parse(value);
        if (type == typeof(Guid)) return Guid.Parse(value);
        if (type.IsEnum) return Enum.Parse(type, value, ignoreCase: true);
        if (type == typeof(DateTime))
        {
            if (value.EndsWith("Z") || value.Contains("+") || value.Contains("-"))
            {
                var dto = DateTimeOffset.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
                return dto.UtcDateTime;
            }
            
            return DateTime.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
        }
        if (type == typeof(DateTimeOffset))
        {
            return DateTimeOffset.Parse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
        }
        return Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
    }
}