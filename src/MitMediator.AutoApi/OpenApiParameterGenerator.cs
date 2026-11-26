using System.Collections;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi;

internal static class OpenApiParameterGenerator
{
    public static IList<OpenApiParameter> GenerateFromType(Type type, string prefix = "")
    {
        var parameters = new List<OpenApiParameter>();
        GenerateRecursive(type, prefix, parameters);
        return parameters;
    }

    public static void RemoveKeyPropertiesFromRequestBody(
        OpenApiOperation operation,
        Type requestType,
        JsonSerializerOptions serializerOptions,
        OpenApiDocument? document = null)
    {
        if (operation.RequestBody?.Content is null)
        {
            return;
        }

        var keyProperties = KeyRequestProperties.GetNames(requestType);
        foreach (var (contentType, mediaType) in operation.RequestBody.Content)
        {
            var schema = mediaType.Schema;
            if (schema is OpenApiSchemaReference reference)
            {
                // ASP.NET may leave the reference unbound until after operation transformers run.
                schema = reference.Target;
                if (schema is null && reference.Reference.Id is { } id &&
                    document?.Components?.Schemas is { } schemas)
                {
                    schemas.TryGetValue(id, out schema);
                }
            }

            if (schema is not OpenApiSchema { Properties: { } properties } source)
            {
                continue;
            }

            var names = contentType == "application/json"
                ? serializerOptions.GetTypeInfo(requestType).Properties
                    .Where(property => property.AttributeProvider is PropertyInfo member && keyProperties.Contains(member.Name))
                    .Select(property => property.Name)
                : keyProperties;
            var namesToRemove = names.Where(properties.ContainsKey).ToArray();
            if (namesToRemove.Length == 0)
            {
                continue;
            }

            // Keep the component intact for any responses or other uses of the DTO.
            var bodySchema = (OpenApiSchema)source.CreateShallowCopy();
            var bodyProperties = new Dictionary<string, IOpenApiSchema>(properties);
            bodySchema.Properties = bodyProperties;
            bodySchema.Required = source.Required?.ToHashSet();
            foreach (var name in namesToRemove)
            {
                bodyProperties.Remove(name);
                bodySchema.Required?.Remove(name);
            }

            mediaType.Schema = bodySchema;
        }
    }

    private static void GenerateRecursive(Type type, string prefix, IList<OpenApiParameter> parameters)
    {
        var keyProperties = KeyRequestProperties.GetNames(type);
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (keyProperties.Contains(prop.Name))
            {
                continue;
            }

            var name = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";
            var isNullable = IsNullable(prop);
            var propType = prop.PropertyType;
            var actualType = Nullable.GetUnderlyingType(propType) ?? propType;

            if (actualType.IsEnum)
            {
                parameters.Add(new OpenApiParameter
                {
                    Name = name,
                    In = ParameterLocation.Query,
                    Required = !isNullable,
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.String,
                        Enum = Enum.GetNames(actualType)
                            .Select(n => (JsonNode)JsonValue.Create(n)!)
                            .ToList()
                    }
                });
            }
            else if (actualType == typeof(string) || actualType.IsPrimitive || actualType == typeof(decimal) || actualType == typeof(DateTime) ||
                     actualType == typeof(DateTimeOffset) || actualType == typeof(Guid))
            {
                parameters.Add(new OpenApiParameter
                {
                    Name = name,
                    In = ParameterLocation.Query,
                    Required = !isNullable,
                    Schema = new OpenApiSchema
                    {
                        Type = MapToOpenApiType(actualType),
                        Format = MapToOpenApiFormat(actualType)
                    }
                });
            }
            else if (actualType.IsArray)
            {
                var itemType = actualType.GetElementType()!;
                var actualItemType = Nullable.GetUnderlyingType(itemType) ?? itemType;

                parameters.Add(new OpenApiParameter
                {
                    Name = name,
                    In = ParameterLocation.Query,
                    Required = !isNullable,
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.Array,
                        Items = CreateItemSchema(actualItemType)
                    },
                    Style = ParameterStyle.Form,
                    Explode = true
                });
            }
            else if (typeof(IEnumerable).IsAssignableFrom(actualType) && actualType.IsGenericType)
            {
                var itemType = actualType.GetGenericArguments()[0];
                var actualItemType = Nullable.GetUnderlyingType(itemType) ?? itemType;

                parameters.Add(new OpenApiParameter
                {
                    Name = name,
                    In = ParameterLocation.Query,
                    Required = !isNullable,
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.Array,
                        Items = CreateItemSchema(actualItemType)
                    },
                    Style = ParameterStyle.Form,
                    Explode = true
                });

                if (IsComplexType(actualItemType))
                {
                    GenerateRecursive(actualItemType, $"{name}[]", parameters);
                }
            }
            else if (IsComplexType(actualType))
            {
                GenerateRecursive(actualType, name, parameters);
            }
        }
    }

    private static OpenApiSchema CreateItemSchema(Type type)
    {
        if (type.IsEnum)
        {
            return new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Enum = Enum.GetNames(type)
                    .Select(n => (JsonNode)JsonValue.Create(n)!)
                    .ToList()
            };
        }

        return new OpenApiSchema
        {
            Type = MapToOpenApiType(type),
            Format = MapToOpenApiFormat(type)
        };
    }

    private static bool IsComplexType(Type type)
    {
        return !type.IsPrimitive
               && type != typeof(string)
               && type != typeof(decimal)
               && type != typeof(DateTime)
               && type != typeof(DateTimeOffset)
               && type != typeof(Guid)
               && !type.IsEnum;
    }

    private static JsonSchemaType MapToOpenApiType(Type type)
    {
        return type switch
        {
            var t when t == typeof(string) => JsonSchemaType.String,
            var t when t == typeof(int) || t == typeof(long) || t == typeof(short) => JsonSchemaType.Integer,
            var t when t == typeof(float) || t == typeof(double) || t == typeof(decimal) => JsonSchemaType.Number,
            var t when t == typeof(bool) => JsonSchemaType.Boolean,
            _ => JsonSchemaType.String
        };
    }

    private static bool IsNullable(PropertyInfo prop)
    {
        var nullabilityContext = new NullabilityInfoContext();
        var nullabilityInfo = nullabilityContext.Create(prop);
        return nullabilityInfo.ReadState == NullabilityState.Nullable;
    }

    private static string? MapToOpenApiFormat(Type type)
    {
        return type switch
        {
            var t when t == typeof(int) => "int32",
            var t when t == typeof(long) => "int64",
            var t when t == typeof(float) => "float",
            var t when t == typeof(double) => "double",
            var t when t == typeof(decimal) => "decimal",
            var t when t == typeof(DateTime) || t == typeof(DateTimeOffset) => "date-time",
            var t when t == typeof(Guid) => "uuid",
            _ => null
        };
    }
}