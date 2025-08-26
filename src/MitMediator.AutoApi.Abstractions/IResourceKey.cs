namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// Represents the final segment of a resource URI. Used to specify the key of a newly created resource.
/// For example, when performing a POST to entities/key1/key2, the Location header of the response
/// might be entities/key1/key2/{last-key}, where {last-key} is the value returned by GetResourceKey().
/// </summary>
public interface IResourceKey
{
    /// <summary>
    /// Get key to resource.
    /// </summary>
    /// <returns>Key.</returns>
    string GetResourceKey();
}