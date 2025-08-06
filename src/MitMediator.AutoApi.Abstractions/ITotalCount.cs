namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// Total count for response.
/// </summary>
public interface ITotalCount
{
    /// <summary>
    /// Get total count.
    /// </summary>
    /// <returns>Total count.</returns>
    int GetTotalCount();

    /// <summary>
    /// Set total count.
    /// </summary>
    /// <param name="totalCount">Total count.</param>
    void SetTotalCount(int totalCount);
}