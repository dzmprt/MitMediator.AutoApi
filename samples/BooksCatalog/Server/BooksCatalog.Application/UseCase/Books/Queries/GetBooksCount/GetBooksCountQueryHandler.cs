using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.UseCase.Books.Queries.GetBooksByFilter;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBooksCount;

/// <summary>
/// Handler for <see cref="GetBooksQuery"/>.
/// </summary>
internal sealed class GetBooksCountQueryHandler : IRequestHandler<GetBooksCountQuery, int>
{
    private readonly IBaseProvider<Book> _booksProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBooksCountQueryHandler"/>.
    /// </summary>
    /// <param name="booksProvider">Books provider.</param>
    public GetBooksCountQueryHandler(IBaseProvider<Book> booksProvider)
    {
        _booksProvider = booksProvider;
    }
    
    /// <inheritdoc/>
    public ValueTask<int> HandleAsync(GetBooksCountQuery request, CancellationToken cancellationToken)
    {
        var freeText = request.FreeText?.Trim().ToUpperInvariant();
        return _booksProvider.CountAsync(
            freeText is null
                ? null
                : q => q.Author.FirstName.Contains(freeText) || 
                       q.Author.LastName.Contains(freeText) || 
                       q.Title.Contains(freeText) || 
                       q.Genre.GenreName.Contains(freeText),
            cancellationToken
        );
    }
}