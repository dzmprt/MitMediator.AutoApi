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

public class CreateByKeyAttribute :
    BaseActionAttribute
{
    public CreateByKeyAttribute(string tag,
        string? version = null,
        string? description = null,
        string? customPattern = null) : base(tag, version, description, customPattern)
    {
    }
}