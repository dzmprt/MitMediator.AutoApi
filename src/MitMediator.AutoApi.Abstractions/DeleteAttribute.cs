namespace MitMediator.AutoApi.Abstractions;

public class DeleteAttribute : BaseActionAttribute
{
    public DeleteAttribute(string tag,
        string? version = null,
        string? description = null,
        string? customPattern = null) : base(tag, version, description, customPattern)
    {
    }
}

public class DeleteByKeyAttribute : BaseActionAttribute
{
    public DeleteByKeyAttribute(string tag,
        string? version = null,
        string? description = null,
        string? customPattern = null) : base(tag, version, description, customPattern)
    {
    }
}