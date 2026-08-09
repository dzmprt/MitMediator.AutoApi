namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// Total count for response.
/// </summary>
public interface ITotalCount
{
    /// <summary>
    /// Total count.
    /// </summary>
    /// <returns>Total count.</returns>
    int TotalCount { get; init; }
}