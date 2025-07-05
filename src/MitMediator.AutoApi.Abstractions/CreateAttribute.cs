namespace MitMediator.AutoApi.Abstractions;

public class CreateAttribute : BaseActionAttribute
{
    public CreateAttribute(string tag,
        string? version = null,
        string? description = null,
        string? customPattern = null) : base(tag, version, description, customPattern)
    {
    }
}