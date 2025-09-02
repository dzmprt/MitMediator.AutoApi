namespace MitMediator.AutoApi.Abstractions.Attributes;

/// <summary>
/// Custom main tag. Example "book" in "api/books/action".
/// </summary>
public class TagAttribute(string tag) : Attribute
{
    /// <summary>
    /// Custom main tag. Example "book" in "api/books/action".
    /// </summary>
    public string Tag { get; } = tag;
}