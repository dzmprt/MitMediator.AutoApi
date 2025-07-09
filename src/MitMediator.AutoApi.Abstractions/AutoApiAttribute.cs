namespace MitMediator.AutoApi.Abstractions;

public class AutoApiAttribute : Attribute
{
    public string? Tag { get; }
    public string? Version { get; }
    public string? Description { get; }
    public string? CustomPattern { get; }
    public string? PatternSuffix { get; set; }
    public HttpMethodType HttpMethodType { get; }

    public AutoApiAttribute(
        string? tag = null, 
        string? version = null, 
        string? description = null, 
        string? customPattern = null,
        string? patternSuffix = null,
        HttpMethodType httpMethodType = HttpMethodType.Auto)
    {
        Tag = tag;
        Version = version;
        Description = description;
        CustomPattern = customPattern;
        PatternSuffix = patternSuffix;
        HttpMethodType = httpMethodType;
        if (!string.IsNullOrWhiteSpace(CustomPattern) && !string.IsNullOrWhiteSpace(PatternSuffix))
        {
            throw new Exception("Suffix can't be specified when a custom pattern is provided.");
        }
    }
}