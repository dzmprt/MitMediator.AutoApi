namespace MitMediator.AutoApi.Abstractions.Attributes;

/// <summary>
/// Custom suffix. Example "cover" in "api/books/cover".
/// </summary>
public sealed class SuffixAttribute(string suffix) : Attribute
{
    /// <summary>
    /// Custom suffix. Example "cover" in "api/books/cover".
    /// </summary>
    public string Suffix { get; set; } = suffix;
}