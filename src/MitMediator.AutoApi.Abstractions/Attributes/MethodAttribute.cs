namespace MitMediator.AutoApi.Abstractions.Attributes;

/// <summary>
/// Custom method type. Example http method "PUT" in "PUT: api/books/1".
/// </summary>
public class MethodAttribute(MethodType methodType) : Attribute
{
    /// <summary>
    /// Custom method type.
    /// </summary>
    public MethodType MethodType { get; } = methodType;
}