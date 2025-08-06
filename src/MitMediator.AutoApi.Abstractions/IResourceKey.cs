namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// Last key to resource.
/// </summary>
/// <typeparam name="TKey">Key type.</typeparam>
public interface IResourceKey
{
    /// <summary>
    /// Get key to resource.
    /// </summary>
    /// <returns>Key.</returns>
    string GetResourceKey();
}