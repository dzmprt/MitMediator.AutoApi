using System.Reflection;
using System.Text;

namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// Helpers for request metadata.
/// </summary>
public static class RequestHelper
{
    public static string BasePath { get; set; }
    
    public static Type GetKeyType(Type queryType)
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

    public static (Type, Type) GetKey2Type(Type queryType)
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

    public static (Type, Type, Type) GetKey3Type(Type queryType)
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

    public static (Type, Type, Type, Type) GetKey4Type(Type queryType)
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

    public static (Type, Type, Type, Type, Type) GetKey5Type(Type queryType)
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

    public static (Type, Type, Type, Type, Type, Type) GetKey6Type(Type queryType)
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

    public static (Type, Type, Type, Type, Type, Type, Type) GetKey7Type(Type queryType)
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

    public static Type GetResponseType(Type queryType)
    {
        var requestInterface = queryType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IRequest<>));

        return requestInterface!.GetGenericArguments()[0];
    }
    
    public static IEnumerable<Type> GetRequestsTypes(Assembly[] assemblies)
    {
        var interfaceType = typeof(IRequest<>);
        return assemblies.SelectMany(a => a.GetTypes())
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType)
                        && t is { IsAbstract: false, IsPublic: true });
    }

    public static int GetKeysCount(Type type)
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

    public static string GetPattern(Type requestType)
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

        var pliralizedTag = GetPluralizedTag(requestType);

        var version = attribute?.Version;
        var pattern = !string.IsNullOrWhiteSpace(attribute?.Version) ? string.Concat(version, "/", pliralizedTag) : pliralizedTag;

        if (isKeyRequest)
        {
            var keysCount = GetKeysCount(requestType);
            pattern = keysCount == 1
                ? string.Concat(pattern, "/{key}")
                : string.Concat(pattern, "/",
                    string.Join("/", Enumerable.Range(1, GetKeysCount(requestType)).Select(c => $"{{key{c}}}")));
        }

        var splitedRequestName = RemoveKeywordsAndSplitRequestNameToKebab(requestType).Split("-");
        if (splitedRequestName.Length > 1)
        {
            var suffixFromType = string.Join("-", splitedRequestName);
            var originTag = GetTag(requestType);
            if (suffixFromType.StartsWith(pliralizedTag) || 
                suffixFromType.StartsWith(originTag) ||
                ((pliralizedTag.EndsWith("s") && suffixFromType.StartsWith(pliralizedTag[..1])) || 
                 pliralizedTag.EndsWith("es")) && suffixFromType.StartsWith(pliralizedTag[..2]))
            {
                var segmentsInTag = pliralizedTag.Split("-").Length;
                suffixFromType = string.Join("-", splitedRequestName[segmentsInTag..]);
            }
            else
            {
                suffixFromType = string.Join("-", splitedRequestName[1..]);
            }

            if (!string.IsNullOrWhiteSpace(suffixFromType))
            {
                pattern = string.Concat(pattern, "/", suffixFromType);   
            }
        }

        if (!string.IsNullOrWhiteSpace(attribute?.PatternSuffix))
        {
            pattern = string.Concat(pattern, "/", attribute.PatternSuffix);
        }

        if (!string.IsNullOrWhiteSpace(BasePath))
        {
            pattern = string.Join("/", BasePath, pattern);
        }

        return pattern;
    }

    private static string GetTag(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<AutoApiAttribute>();
        var tag = attribute?.Tag;
        if (string.IsNullOrWhiteSpace(tag))
        {
            var requestNameToKebab = RemoveKeywordsAndSplitRequestNameToKebab(requestType);
            tag = requestNameToKebab.Split("-").First();
            return tag;
        }

        return SplitPascalCaseToKebab(tag);
    }

    public static string GetPluralizedTag(Type requestType)
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

    public static HttpMethodType GetHttpMethod(Type requestType)
    {
        foreach (var kvp in ActionNamesMaps)
        {
            if (requestType.Name.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase))
                return kvp.Value;
        }

        return HttpMethodType.Get;
    }

    private static readonly Dictionary<string, HttpMethodType> ActionNamesMaps = new(StringComparer.InvariantCultureIgnoreCase)
    {
        ["get"] = HttpMethodType.Get,
        ["load"] = HttpMethodType.Get,
        ["download"] = HttpMethodType.Get,
        ["fetch"] = HttpMethodType.Get,

        ["update"] = HttpMethodType.Put,
        ["change"] = HttpMethodType.Put,
        ["edit"] = HttpMethodType.Put,
        ["modify"] = HttpMethodType.Put,
        ["put"] = HttpMethodType.Put,

        ["post"] = HttpMethodType.Post,
        ["import"] = HttpMethodType.Post,
        ["upload"] = HttpMethodType.Post,

        ["add"] = HttpMethodType.PostCreate,
        ["create"] = HttpMethodType.PostCreate,

        ["delete"] = HttpMethodType.Delete,
        ["remove"] = HttpMethodType.Delete,
        ["drop"] = HttpMethodType.Delete
    };

    private static string RemoveActionName(string name)
    {
        foreach (var kvp in ActionNamesMaps.Where(kvp => name.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase)))
        {
            return name[kvp.Key.Length..];
        }

        return name;
    }

    private static string RemoveKeywordsAndSplitRequestNameToKebab(Type requestType)
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
        for (var i = 0; i < input.Length; i++)
        {
            var c = input[i];
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
}