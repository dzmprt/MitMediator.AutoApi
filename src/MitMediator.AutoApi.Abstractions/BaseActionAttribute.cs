namespace MitMediator.AutoApi.Abstractions;

public abstract class BaseActionAttribute : Attribute
{
    public string? CustomPattern { get; }
    public string Tag { get; }
    public string? Version { get; }
    public string? Description { get; }

    public BaseActionAttribute(string tag, string? version, string? description, string? customPattern)
    {
        CustomPattern = customPattern;
        Tag = tag;
        Version = version;
        Description = description;
    }
}