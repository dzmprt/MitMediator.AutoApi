using MitMediator;

namespace Application.UseCase.Books.Queries.GetBooksCount;

/// <summary>
/// Get books count query.
/// </summary>
public class GetBooksCountQuery : IRequest<int>
{
    /// <summary>
    /// Free text.
    /// </summary>
    public string? FreeText { get; init; }
}