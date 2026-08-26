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
    long TotalCount { get; init; }
}