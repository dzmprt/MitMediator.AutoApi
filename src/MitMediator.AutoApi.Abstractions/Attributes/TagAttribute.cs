namespace MitMediator.AutoApi.Abstractions.Attributes;

/// <summary>
/// Custom main tag. Example "book" in "api/books/action".
/// </summary>
public sealed class TagAttribute(string tag) : Attribute
{
    /// <summary>
    /// Custom main tag. Example "book" in "api/books/action".
    /// </summary>
    public string Tag { get; } = tag;
}