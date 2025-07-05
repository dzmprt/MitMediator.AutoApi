namespace MitMediator.AutoApi.Abstractions;

public class PostAttribute : BaseActionAttribute
{
    public PostAttribute(string tag,
        string? version = null,
        string? description = null,
        string? customPattern = null) : base(tag, version, description, customPattern)
    {
    }
}