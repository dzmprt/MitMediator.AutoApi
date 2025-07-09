using Application.Abstractions.Infrastructure;
using Application.UseCase.Authors.Queries.GetAuthorsByFilter;
using Domain;
using MitMediator;

namespace Application.UseCase.Books.Queries.GetBooksByFilter;

/// <summary>
/// Handler for <see cref="GetBooksQuery"/>.
/// </summary>
internal sealed class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, Book[]>
{
    private readonly IBaseProvider<Book> _booksProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAuthorsQueryHandler"/>.
    /// </summary>
    /// <param name="booksProvider">Books provider.</param>
    public GetBooksQueryHandler(IBaseProvider<Book> booksProvider)
    {
        _booksProvider = booksProvider;
    }
    
    /// <inheritdoc/>
    public ValueTask<Book[]> HandleAsync(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var freeText = request.FreeText?.Trim().ToUpperInvariant();
        return _booksProvider.SearchAsync(
            freeText is null
                ? null
                : q => q.Author.FirstName.Contains(freeText) || 
                       q.Author.LastName.Contains(freeText) || 
                       q.Title.Contains(freeText) || 
                       q.Genre.GenreName.Contains(freeText),
            o => o.BookId,
            request.Limit,
            request.Offset,
            cancellationToken
        );
    }
}