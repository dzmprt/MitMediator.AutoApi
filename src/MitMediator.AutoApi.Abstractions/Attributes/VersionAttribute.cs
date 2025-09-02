namespace MitMediator.AutoApi.Abstractions.Attributes;

/// <summary>
/// Custom API version. Example "v2" in "api/v2/books/action".
/// </summary>
public class VersionAttribute(string version) : Attribute
{
    /// <summary>
    /// Custom API version. Example "v2" in "api/v2/books/action".
    /// </summary>
    public string Version { get; } = version;
}