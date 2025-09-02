namespace MitMediator.AutoApi.Abstractions.Attributes;

/// <summary>
/// Custom endpoint description.
/// </summary>
public class DescriptionAttribute(string description) : Attribute
{
    /// <summary>
    /// Custom endpoint description.
    /// </summary>
    public string Description { get; } = description;
}