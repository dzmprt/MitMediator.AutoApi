using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace MitMediator.AutoApi.HttpMediator;

internal class PrivateSetterResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var typeInfo = base.GetTypeInfo(type, options);

        foreach (var prop in typeInfo.Properties)
        {
            if (prop.Set is null)
            {
                var propertyInfo = type.GetProperty(prop.Name,
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                var setter = propertyInfo?.SetMethod;
                if (setter is not null)
                {
                    prop.Set = (obj, value) => setter.Invoke(obj, new object[] { value });
                }
            }
        }

        return typeInfo;
    }
}