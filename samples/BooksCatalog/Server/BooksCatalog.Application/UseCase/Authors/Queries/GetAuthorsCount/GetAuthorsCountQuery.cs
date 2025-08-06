using MitMediator;

namespace BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsCount;

/// <summary>
/// Get authors count query.
/// </summary>
public struct GetAuthorsCountQuery : IRequest<int>
{
    /// <summary>
    /// Free text.
    /// </summary>
    public string? FreeText { get; init; }
}