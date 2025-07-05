namespace MitMediator.AutoApi.Abstractions;

public class UpdateAttribute : BaseActionAttribute
{
    public UpdateAttribute(string tag,
        string? version = null,
        string? description = null,
        string? customPattern = null) : base(tag, version, description, customPattern)
    {
    }
}

public class UpdateByKeyAttribute :
    BaseActionAttribute
{
    public UpdateByKeyAttribute(string tag,
        string? version = null,
        string? description = null,
        string? customPattern = null) : base(tag, version, description, customPattern)
    {
    }
}