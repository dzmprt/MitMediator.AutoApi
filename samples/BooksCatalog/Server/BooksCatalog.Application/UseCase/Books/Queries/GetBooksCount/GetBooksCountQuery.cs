using MitMediator;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBooksCount;

/// <summary>
/// Get books count query.
/// </summary>
public struct GetBooksCountQuery : IRequest<int>
{
    /// <summary>
    /// Free text.
    /// </summary>
    public string? FreeText { get; init; }
}