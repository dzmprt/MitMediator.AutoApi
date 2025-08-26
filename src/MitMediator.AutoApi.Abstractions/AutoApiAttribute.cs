namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// Attribute for customize API endpoint.
/// </summary>
public class AutoApiAttribute : Attribute
{
    /// <summary>
    /// Main tag. Example TAG in "api/tag/action".
    /// </summary>
    public string? Tag { get; }
    
    /// <summary>
    /// API version. Used url and swagger. Example V2 in "api/v2/tag/action".
    /// </summary>
    public string? Version { get; }
    
    /// <summary>
    /// Endpoint description.
    /// </summary>
    public string? Description { get; }
    
    /// <summary>
    /// Custom pattern. If set, base url, version and tag will be ignored.
    /// </summary>
    public string? CustomPattern { get; }
    
    /// <summary>
    /// Pattern suffix. Example ACTION in "api/tag/action"
    /// </summary>
    public string? PatternSuffix { get; set; }
    
    /// <summary>
    /// Set Http Method.
    /// </summary>
    public HttpMethodType HttpMethodType { get; }
    
    /// <summary>
    /// Custom HTTP response ContentType.
    /// </summary>
    public string? CustomResponseContentType { get; }

    /// <summary>
    /// Create AutoApiAttribute.
    /// </summary>
    /// <param name="tag"><see cref="Tag"/></param>
    /// <param name="version"><see cref="Version"/></param>
    /// <param name="description"><see cref="Description"/></param>
    /// <param name="customPattern"><see cref="CustomPattern"/></param>
    /// <param name="patternSuffix"><see cref="PatternSuffix"/></param>
    /// <param name="customResponseContentType"><see cref="CustomResponseContentType"/></param>
    /// <param name="httpMethodType"><see cref="HttpMethodType"/></param>
    /// <exception cref="Exception">Suffix can't be specified when a custom pattern is provided.</exception>
    public AutoApiAttribute(
        string? tag = null, 
        string? version = null, 
        string? description = null, 
        string? customPattern = null,
        string? patternSuffix = null,
        string? customResponseContentType = null,
        HttpMethodType httpMethodType = HttpMethodType.Auto)
    {
        Tag = tag;
        Version = version;
        Description = description;
        CustomPattern = customPattern;
        PatternSuffix = patternSuffix;
        HttpMethodType = httpMethodType;
        CustomResponseContentType = customResponseContentType;
        if (!string.IsNullOrWhiteSpace(CustomPattern) && !string.IsNullOrWhiteSpace(PatternSuffix))
        {
            throw new Exception("Suffix can't be specified when a custom pattern is provided.");
        }
    }
}