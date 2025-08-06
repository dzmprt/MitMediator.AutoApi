using System.Linq.Expressions;
using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.UseCase.Authors.Queries.GetAuthorsByFilter;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBooksByFilter;

/// <summary>
/// Handler for <see cref="GetBooksQuery"/>.
/// </summary>
internal sealed class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, GetBooksResponse>
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
    public async ValueTask<GetBooksResponse> HandleAsync(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var freeText = request.FreeText?.Trim().ToUpperInvariant();
        Expression<Func<Book, bool>> searchExpression = q => string.IsNullOrWhiteSpace(freeText) ||
                                                              q.Author.FirstName.Contains(freeText) ||
                                                              q.Author.LastName.Contains(freeText) ||
                                                              q.Title.Contains(freeText) ||
                                                              q.Genre.GenreName.Contains(freeText);
        var items = await _booksProvider.SearchAsync(searchExpression,
            o => o.BookId,
            request.Limit,
            request.Offset,
            cancellationToken
        );
        
        var totalCount = await _booksProvider.CountAsync(searchExpression, cancellationToken);
        var response = new GetBooksResponse()
        {
            Items = items,
        };
        response.SetTotalCount(totalCount);
        return response;
    }
}