using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Books.Queries.GetBooksByFilter;

/// <summary>
/// Get books query.
/// </summary>
public class GetBooksQuery : IRequest<Book[]>
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