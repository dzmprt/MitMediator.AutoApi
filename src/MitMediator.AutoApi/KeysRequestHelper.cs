using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

internal static class KeysRequestHelper
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
    
    internal static string GetKeyPattern(BaseActionAttribute arrt, Type requestType)
    {
        var keysCount = GetKeysCount(requestType);
        if (keysCount == 1)
        {
            if (arrt.CustomPattern is not null)
            {
                if (!arrt.CustomPattern.Contains("{key}"))
                {
                    throw new Exception("Custom pattern must contain '{key}'.");
                }

                return arrt.CustomPattern;
            }

            var basePattern = Helpers.GetBasePattern(arrt);
            return string.Concat(basePattern, "/{key}");
        }
        else
        {
            if (arrt.CustomPattern is not null)
            {
                for (var i = 1; i < keysCount + 1; i++)
                {
                    if (!arrt.CustomPattern.Contains($"{{key{i}}}"))
                    {
                        throw new Exception($"Custom pattern must contain '{{key{i}}}'.");
                    }
                }

                return arrt.CustomPattern;
            }

            var basePattern = Helpers.GetBasePattern(arrt);
            return string.Concat(basePattern,
                string.Join("", Enumerable.Range(1, keysCount).Select(c => $"/{{key{c}}}")));
        }
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

        return keyInterface?.GenericTypeArguments.Length ?? throw new Exception($"Invalid number of generic arguments for type {type.Name}. Type must implement IKeyRequest<> interface.");
    }
}