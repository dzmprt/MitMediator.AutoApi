using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace MitMediator.AutoApi;

internal static class OpenApiParameterGenerator
{
    public static IList<OpenApiParameter> GenerateFromType(Type type, string prefix = "")
    {
        var parameters = new List<OpenApiParameter>();
        GenerateRecursive(type, prefix, parameters);
        return parameters;
    }

    private static void GenerateRecursive(Type type, string prefix, IList<OpenApiParameter> parameters)
    {
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
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
                        Type = "string",
                        Enum = Enum.GetNames(actualType)
                            .Select(n => new OpenApiString(n))
                            .ToList<IOpenApiAny>()
                    }
                });
            }
            else if (actualType == typeof(string) || actualType.IsPrimitive || actualType == typeof(DateTime) ||
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
                        Type = "array",
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
                        Type = "array",
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
                Type = "string",
                Enum = Enum.GetNames(type).Select(n => new OpenApiString(n)).ToList<IOpenApiAny>()
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
               && type != typeof(DateTime)
               && type != typeof(DateTimeOffset)
               && type != typeof(Guid)
               && !type.IsEnum;
    }

    private static string MapToOpenApiType(Type type)
    {
        return type switch
        {
            var t when t == typeof(string) => "string",
            var t when t == typeof(int) || t == typeof(long) || t == typeof(short) => "integer",
            var t when t == typeof(float) || t == typeof(double) || t == typeof(decimal) => "number",
            var t when t == typeof(bool) => "boolean",
            var t when t == typeof(DateTime) || t == typeof(DateTimeOffset) => "string",
            var t when t == typeof(Guid) => "string",
            _ => "string"
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