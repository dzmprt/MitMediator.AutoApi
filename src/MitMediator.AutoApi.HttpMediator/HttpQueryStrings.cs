using System.Reflection;
using System.Text;

namespace MitMediator.AutoApi.HttpMediator;

public static class HttpQueryStrings
{
    public static string ToQueryString(this object obj)
    {
        var query = new StringBuilder();

        BuildQueryString(obj, query, "");

        if (query.Length > 0) query[0] = '?';

        return query.ToString();
    }

    private static void BuildQueryString(object? obj, StringBuilder query, string prefix = "")
    {
        if (obj == null) return;

        foreach (var p in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (p.GetValue(obj, Array.Empty<object>()) == null)
            {
                continue;
            }

            var value = p.GetValue(obj, Array.Empty<object>());


            if (p.PropertyType.IsArray && value?.GetType() == typeof(DateTime[]))
                foreach (var item in (DateTime[])value)
                    query.Append($"&{prefix}{p.Name}={item.ToString("yyyy-MM-dd")}");

            else if (p.PropertyType.IsArray)
                foreach (var item in (Array)value!)
                    query.Append($"&{prefix}{p.Name}={item}");

            else if (p.PropertyType == typeof(string))
                query.Append($"&{prefix}{p.Name}={value}");

            else if (p.PropertyType == typeof(DateTime) && !value!.Equals(Activator.CreateInstance(p.PropertyType))) // is not default 
                query.Append($"&{prefix}{p.Name}={((DateTime)value).ToString("yyyy-MM-dd")}");

            else if (p.PropertyType.IsValueType && !value!.Equals(Activator.CreateInstance(p.PropertyType))) // is not default 
                query.Append($"&{prefix}{p.Name}={value}");


            else if (p.PropertyType.IsClass)
                BuildQueryString(value, query, $"{prefix}{p.Name}.");
        }
    }
}