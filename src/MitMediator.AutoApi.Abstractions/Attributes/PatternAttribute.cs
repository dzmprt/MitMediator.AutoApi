namespace MitMediator.AutoApi.Abstractions.Attributes;

/// <summary>
/// Custom pattern. If set, base url, version and tag will be ignored.
/// </summary>
public sealed class PatternAttribute(string pattern) : Attribute
{
    /// <summary>
    /// Custom pattern. If set, base url, version and tag will be ignored.
    /// </summary>
    public string Pattern { get; } = pattern;
}