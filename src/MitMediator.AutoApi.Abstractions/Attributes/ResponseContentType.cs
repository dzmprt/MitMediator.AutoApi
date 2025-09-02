namespace MitMediator.AutoApi.Abstractions.Attributes;

/// <summary>
/// Custom response content type. Example "image/png".
/// </summary>
public class ResponseContentTypeAttribute(string responseContentType) : Attribute
{
    /// <summary>
    /// Custom response content type. Example "image/png".
    /// </summary>
    public string ResponseContentType { get; } = responseContentType;
}