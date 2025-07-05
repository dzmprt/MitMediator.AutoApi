namespace MitMediator.AutoApi.Abstractions;

public class GetAttribute : 
    BaseActionAttribute
{
    public GetAttribute(string tag, 
        string? version = null, 
        string? description = null,
        string? customPattern = null) : base(tag, version, description, customPattern)
    {
    }
}

public class GetByKeyAttribute :
    BaseActionAttribute
{
    public GetByKeyAttribute(string tag,
        string? version = null,
        string? description = null,
        string? customPattern = null) : base(tag, version, description, customPattern)
    {
    }
}