using System.Reflection;
using System.Text;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

internal static class Helpers
{
    internal static Type GetKeyType(Type queryType)
    {
        var requestKeyInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IKeyRequest<>));

        if (requestKeyInterface == null)
        {
            throw new Exception($"{queryType.Name} must implement IKeyRequest<> interface.");
        }

        return requestKeyInterface!.GetGenericArguments()[0];
    }

    internal static (Type, Type) GetKey2Type(Type queryType)
    {
        var requestKeyInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IKeyRequest<,>));

        if (requestKeyInterface == null)
        {
            throw new Exception($"{queryType.Name} must implement IKeyRequest<,> interface.");
        }

        var keyTypes = requestKeyInterface!.GetGenericArguments();

        return (keyTypes[0], keyTypes[1]);
    }

    internal static (Type, Type, Type) GetKey3Type(Type queryType)
    {
        var requestKeyInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IKeyRequest<,,>));

        if (requestKeyInterface == null)
        {
            throw new Exception($"{queryType.Name} must implement IKeyRequest<,,> interface.");
        }

        var keyTypes = requestKeyInterface!.GetGenericArguments();

        return (keyTypes[0], keyTypes[1], keyTypes[2]);
    }

    internal static (Type, Type, Type, Type) GetKey4Type(Type queryType)
    {
        var requestKeyInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IKeyRequest<,,,>));

        if (requestKeyInterface == null)
        {
            throw new Exception($"{queryType.Name} must implement IKeyRequest<,,,> interface.");
        }

        var keyTypes = requestKeyInterface!.GetGenericArguments();

        return (keyTypes[0], keyTypes[1], keyTypes[2], keyTypes[3]);
    }

    internal static (Type, Type, Type, Type, Type) GetKey5Type(Type queryType)
    {
        var requestKeyInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IKeyRequest<,,,,>));

        if (requestKeyInterface == null)
        {
            throw new Exception($"{queryType.Name} must implement IKeyRequest<,,,,> interface.");
        }

        var keyTypes = requestKeyInterface!.GetGenericArguments();

        return (keyTypes[0], keyTypes[1], keyTypes[2], keyTypes[3], keyTypes[4]);
    }

    internal static (Type, Type, Type, Type, Type, Type) GetKey6Type(Type queryType)
    {
        var requestKeyInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IKeyRequest<,,,,,>));

        if (requestKeyInterface == null)
        {
            throw new Exception($"{queryType.Name} must implement IKeyRequest<,,,,,> interface.");
        }

        var keyTypes = requestKeyInterface!.GetGenericArguments();

        return (keyTypes[0], keyTypes[1], keyTypes[2], keyTypes[3], keyTypes[4], keyTypes[5]);
    }

    internal static (Type, Type, Type, Type, Type, Type, Type) GetKey7Type(Type queryType)
    {
        var requestKeyInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IKeyRequest<,,,,,,>));

        if (requestKeyInterface == null)
        {
            throw new Exception($"{queryType.Name} must implement IKeyRequest<,,,,,,> interface.");
        }

        var keyTypes = requestKeyInterface!.GetGenericArguments();

        return (keyTypes[0], keyTypes[1], keyTypes[2], keyTypes[3], keyTypes[4], keyTypes[5], keyTypes[6]);
    }

    public static bool IsKeyRequest(Type requestType)
    {
        return requestType.GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                new[]
                {
                    typeof(IKeyRequest<>),
                    typeof(IKeyRequest<,>),
                    typeof(IKeyRequest<,,>),
                    typeof(IKeyRequest<,,,>),
                    typeof(IKeyRequest<,,,,>),
                    typeof(IKeyRequest<,,,,,>),
                    typeof(IKeyRequest<,,,,,,>),
                }.Contains(i.GetGenericTypeDefinition())) != null;
    }

    internal static int GetKeysCount(Type type)
    {
        var keyInterface = type.GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                new[]
                {
                    typeof(IKeyRequest<>),
                    typeof(IKeyRequest<,>),
                    typeof(IKeyRequest<,,>),
                    typeof(IKeyRequest<,,,>),
                    typeof(IKeyRequest<,,,,>),
                    typeof(IKeyRequest<,,,,,>),
                    typeof(IKeyRequest<,,,,,,>),
                }.Contains(i.GetGenericTypeDefinition()));

        return keyInterface?.GenericTypeArguments.Length ?? throw new Exception(
            $"Invalid number of generic arguments for type {type.Name}. Type must implement IKeyRequest<> interface.");
    }

    internal static Type GetResponseType(Type queryType)
    {
        var requestInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IRequest<>));

        return requestInterface!.GetGenericArguments()[0];
    }

    internal static IEnumerable<Type> GetRequestsTypes(Assembly[] assemblies)
    {
        var interfaceType = typeof(IRequest<>);
        return assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                            && t is { IsAbstract: false, IsPublic: true });
    }

    internal static IEnumerable<Type> GetTypesWithAttribute<TAttribute>()
        where TAttribute : Attribute
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type!.GetCustomAttributes(typeof(TAttribute), inherit: true).Any())!;
    }

    internal static string GetPattern(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<AutoApiAttribute>();
        var isKeyRequest = IsKeyRequest(requestType);
        if (attribute?.CustomPattern is not null)
        {
            if (!isKeyRequest)
            {
                return attribute.CustomPattern;
            }
            
            var keysCount = GetKeysCount(requestType);
            if (keysCount == 1)
            {
                if (!attribute.CustomPattern.Contains("{key}"))
                {
                    throw new Exception("Custom pattern must contain '{key}'.");
                }

                return attribute.CustomPattern;
            }

            for (var i = 1; i < keysCount + 1; i++)
            {
                if (!attribute.CustomPattern.Contains($"{{key{i}}}"))
                {
                    throw new Exception($"Custom pattern must contain '{{key{i}}}'.");
                }
            }

            return attribute.CustomPattern;
        }

        var tag = GetTag(requestType);

        var version = attribute?.Version;
        var pattern = !string.IsNullOrWhiteSpace(attribute?.Version) ? string.Concat(version, "/", tag) : tag;

        if (isKeyRequest)
        {
            var keysCount = GetKeysCount(requestType);
            pattern = keysCount == 1 ? string.Concat(pattern, "/{key}") : 
                string.Concat(pattern, "/", string.Join("/",  Enumerable.Range(1, GetKeysCount(requestType)).Select(c => $"{{key{c}}}")));
        }

        var splitedRequestName = RemoveKeywordsAndSplitRequestNameToKebab(requestType).Split("-");
        if (splitedRequestName.Length > 1)
        {
            var suffixFromType = string.Join("-", splitedRequestName[1..]);
            pattern = string.Concat(pattern, "/", suffixFromType);
        }
        
        if (!string.IsNullOrWhiteSpace(attribute?.PatternSuffix))
        {
            pattern = string.Concat(pattern, "/", attribute.PatternSuffix);
        }

        return pattern;
    }

    public static string GetTag(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<AutoApiAttribute>();
        var tag = attribute?.Tag;
        if (string.IsNullOrWhiteSpace(tag))
        {
            var requestNameToKebab = RemoveKeywordsAndSplitRequestNameToKebab(requestType);
            tag = requestNameToKebab.Split("-").First();
            return Pluralizer.Pluralize(tag);
        }
        return SplitPascalCaseToKebab(tag);
    }
    
    public static string RemoveKeywordsAndSplitRequestNameToKebab(Type requestType)
    {
        var name = RemoveActionName(requestType.Name);

        name = requestType.Name switch
        {
            var s when s.EndsWith("command", StringComparison.OrdinalIgnoreCase) => 
                name.Substring(0, name.Length - "command".Length),
            var s when s.EndsWith("query", StringComparison.OrdinalIgnoreCase) => 
                name.Substring(0, name.Length - "query".Length),
            var s when s.EndsWith("request", StringComparison.OrdinalIgnoreCase) => 
                name.Substring(0, name.Length - "request".Length),
            _ => name
        };
        name = SplitPascalCaseToKebab(name);
        return name;
    }

    private static string SplitPascalCaseToKebab(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var builder = new StringBuilder();
        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];
            if (char.IsUpper(c))
            {
                if (i > 0)
                    builder.Append('-');

                builder.Append(char.ToLowerInvariant(c));
            }
            else
            {
                builder.Append(c);
            }
        }

        return builder.ToString();
    }
    
    internal static HttpMethodType GetHttpMethodType(Type requestType)
    {
        foreach (var kvp in ActionNamesMaps)
        {
            if (requestType.Name.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase))
                return kvp.Value;
        }

        return HttpMethodType.Get;
    }
    
    internal static string RemoveActionName(string name)
    {
        foreach (var kvp in ActionNamesMaps)
        {
            if (name.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase))
                return name.Substring(kvp.Key.Length);
        }

        return name;
    }

    public static Dictionary<string, HttpMethodType> ActionNamesMaps = new(StringComparer.OrdinalIgnoreCase)
    {
        ["get"] = HttpMethodType.Get,
        ["load"] = HttpMethodType.Get,
        ["download"] = HttpMethodType.Get,

        ["update"] = HttpMethodType.Put,
        ["change"] = HttpMethodType.Put,
        ["edit"] = HttpMethodType.Put,
        ["modify"] = HttpMethodType.Put,
        ["put"] = HttpMethodType.Put,

        ["post"] = HttpMethodType.Post,

        ["add"] = HttpMethodType.PostCreate,
        ["create"] = HttpMethodType.PostCreate,
        ["upload"] = HttpMethodType.PostCreate,

        ["delete"] = HttpMethodType.Delete,
        ["remove"] = HttpMethodType.Delete,
        ["drop"] = HttpMethodType.Delete
    };
}