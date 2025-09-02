using System.Reflection;
using System.Text;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// Request info.
/// </summary>
public class RequestInfo
{
    public string BasePath { get; set; }

    /// <summary>
    /// Main tag. Example TAG in "api/tag/action".
    /// </summary>
    public string Tag { get; }

    /// <summary>
    /// Pluralized main tag. Example TAG in "api/tag/action".
    /// </summary>
    public string PluralizedTag { get; }

    /// <summary>
    /// API version. Used url and swagger. Example V2 in "api/v2/tag/action".
    /// </summary>
    public string Version { get; }

    /// <summary>
    /// Endpoint description.
    /// </summary>
    public string? Description { get; }

    /// <summary>
    /// Custom pattern. If set, base url, version and tag will be ignored.
    /// </summary>
    public string Pattern { get; }

    // /// <summary>
    // /// Pattern suffix. Example ACTION in "api/tag/action"
    // /// </summary>
    public string? Suffix { get; set; }

    /// <summary>
    /// Set Http Method.
    /// </summary>
    public MethodType MethodType { get; }

    /// <summary>
    /// Custom HTTP response ContentType.
    /// </summary>
    public string? ContentType { get; }

    public Type RequestType { get; }

    public bool IsIgnored { get; }

    public Type ResponseType { get; }

    public bool IsKeyRequest { get; }

    public int? KeysCount { get; }
    
    public bool IsDisableAntiforgery { get; }

    /// <summary>
    /// Create RequestInfo.
    /// </summary>
    // /// <exception cref="Exception">Suffix can't be specified when a custom pattern is provided.</exception>
    public RequestInfo(Type requestType, string? basePath = null)
    {
        BasePath = basePath;
        RequestType = requestType;
        Tag = GetTag(requestType);
        Version = GetVersion(requestType);
        Description = GetDescription(requestType);
        PluralizedTag = GetPluralizedTag(requestType);
        Pattern = GetPattern(requestType, basePath);
        Suffix = GetSuffix(requestType);
        MethodType = GetHttpMethod(requestType);
        ContentType = GetContentType(requestType);
        IsIgnored = GetIsIgnored(requestType);
        ResponseType = GetResponseType(requestType);
        IsKeyRequest = GetIsKeyRequest(requestType);
        IsDisableAntiforgery = GetIsDisableAntiforgery(requestType);
        if (IsKeyRequest)
        {
            KeysCount = GetKeysCount(requestType);
        }
    }

    private static bool GetIsDisableAntiforgery(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<DisableAntiforgeryAttribute>();
        return attribute is not null;
    }
    
    private static string? GetContentType(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<ResponseContentTypeAttribute>();
        return attribute?.ResponseContentType;
    }

    private static bool GetIsIgnored(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<IgnoreRequestAttribute>();
        return attribute != null;
    }

    private static string GetTag(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<TagAttribute>();
        var tag = attribute?.Tag;
        if (string.IsNullOrWhiteSpace(tag))
        {
            var requestNameToKebab = RemoveKeywordsAndSplitToKebab(requestType.Name);
            tag = requestNameToKebab.Split("-").First();
            return tag;
        }

        return SplitPascalCaseToKebab(tag);
    }

    private static string GetVersion(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<VersionAttribute>();
        var version = attribute?.Version;
        if (string.IsNullOrWhiteSpace(version))
        {
            version = "v1";
        }

        return version;
    }

    private static string? GetDescription(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description;
    }

    public static string GetPattern(Type requestType, string? basePath)
    {
        var patternAttribute = requestType.GetCustomAttribute<PatternAttribute>();
        var isKeyRequest = GetIsKeyRequest(requestType);
        if (patternAttribute?.Pattern is not null)
        {
            if (!isKeyRequest)
            {
                return patternAttribute.Pattern;
            }

            var keysCount = GetKeysCount(requestType);
            if (keysCount == 1)
            {
                if (!patternAttribute.Pattern.Contains("{key}"))
                {
                    throw new Exception("Custom pattern must contain '{key}'.");
                }

                return patternAttribute.Pattern;
            }

            for (var i = 1; i < keysCount + 1; i++)
            {
                if (!patternAttribute.Pattern.Contains($"{{key{i}}}"))
                {
                    throw new Exception($"Custom pattern must contain '{{key{i}}}'.");
                }
            }

            return patternAttribute.Pattern;
        }

        var pliralizedTag = GetPluralizedTag(requestType);
        var version = GetVersion(requestType);

        var pattern = !string.IsNullOrWhiteSpace(version)
            ? string.Concat(version, "/", pliralizedTag)
            : pliralizedTag;

        if (isKeyRequest)
        {
            var keysCount = GetKeysCount(requestType);
            pattern = keysCount == 1
                ? string.Concat(pattern, "/{key}")
                : string.Concat(pattern, "/",
                    string.Join("/", Enumerable.Range(1, keysCount).Select(c => $"{{key{c}}}")));
        }

        var suffix = GetSuffix(requestType);

        if (!string.IsNullOrWhiteSpace(suffix))
        {
            pattern = string.Concat(pattern, "/", suffix);
        }

        if (!string.IsNullOrWhiteSpace(basePath))
        {
            pattern = string.Join("/", basePath, pattern);
        }

        return pattern;
    }

    private static string? GetSuffix(Type requestType)
    {
        var patternAttribute = requestType.GetCustomAttribute<PatternAttribute>();
        var suffix = requestType.GetCustomAttribute<SuffixAttribute>();
        
        if (patternAttribute is not null && suffix is not null)
        {
            throw new Exception("Suffix can't be specified when a custom pattern is provided.");
        }
        
        if (suffix != null)
        {
            return suffix.Suffix;
        }

        var pliralizedTag = GetPluralizedTag(requestType);
        var splitedRequestName = RemoveKeywordsAndSplitToKebab(requestType.Name).Split("-");
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

            return suffixFromType;
        }

        return null;
    }

    public static bool GetIsKeyRequest(Type requestType)
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

    public static int GetKeysCount(Type requestType)
    {
        var keyInterface = requestType.GetInterfaces()
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
            $"Invalid number of generic arguments for type {requestType.Name}. Type must implement IKeyRequest<> interface.");
    }

    private static string GetPluralizedTag(Type requestType)
    {
        var attribute = requestType.GetCustomAttribute<TagAttribute>();
        var tag = attribute?.Tag;
        if (!string.IsNullOrWhiteSpace(tag))
        {
            return SplitPascalCaseToKebab(tag);
        }
        var requestNameToKebab = RemoveKeywordsAndSplitToKebab(requestType.Name);
        tag = requestNameToKebab.Split("-").First();
        return SplitPascalCaseToKebab(Pluralizer.Pluralize(tag));
    }

    private static string RemoveKeywordsAndSplitToKebab(string name)
    {
        name = RemoveActionName(name);

        name = name switch
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

    private static string RemoveActionName(string name)
    {
        foreach (var kvp in ActionNamesMaps.Where(kvp => name.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase)))
        {
            return name[kvp.Key.Length..];
        }

        return name;
    }

    public static MethodType GetHttpMethod(Type requestType)
    {
        var methodAttribute = requestType.GetCustomAttribute<MethodAttribute>();
        if (methodAttribute is not null)
        {
            return methodAttribute.MethodType;
        }
        var typeValue = MethodType.Get;
        foreach (var kvp in ActionNamesMaps)
        {
            if (requestType.Name.StartsWith(kvp.Key, StringComparison.OrdinalIgnoreCase))
                typeValue = kvp.Value;
        }

        if (typeof(IFileRequest).IsAssignableFrom(requestType) && typeValue != MethodType.Post &&
            typeValue != MethodType.PostCreate)
        {
            return MethodType.Post;
        }

        return typeValue;
    }

    private static readonly Dictionary<string, MethodType> ActionNamesMaps =
        new(StringComparer.InvariantCultureIgnoreCase)
        {
            ["get"] = MethodType.Get,
            ["load"] = MethodType.Get,
            ["download"] = MethodType.Get,
            ["fetch"] = MethodType.Get,

            ["update"] = MethodType.Put,
            ["change"] = MethodType.Put,
            ["edit"] = MethodType.Put,
            ["modify"] = MethodType.Put,
            ["put"] = MethodType.Put,

            ["post"] = MethodType.Post,
            ["import"] = MethodType.Post,
            ["upload"] = MethodType.Post,

            ["add"] = MethodType.PostCreate,
            ["create"] = MethodType.PostCreate,

            ["delete"] = MethodType.Delete,
            ["remove"] = MethodType.Delete,
            ["drop"] = MethodType.Delete
        };

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

    public static Type GetResponseType(Type requestType)
    {
        var requestInterface = requestType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IRequest<>));

        return requestInterface!.GetGenericArguments()[0];
    }
}