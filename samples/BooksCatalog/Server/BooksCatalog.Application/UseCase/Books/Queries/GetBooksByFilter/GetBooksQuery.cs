using MitMediator;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBooksByFilter;

/// <summary>
/// Get books query.
/// </summary>
public struct GetBooksQuery : IRequest<GetBooksResponse>
{
    /// <summary>
    /// Limit.
    /// </summary>
    public int? Limit { get; init; }
    
    /// <summary>
    /// Offset.
    /// </summary>
    public int? Offset { get; init; }
    
    /// <summary>
    /// Free text.
    /// </summary>
    public string? FreeText { get; init; }
}