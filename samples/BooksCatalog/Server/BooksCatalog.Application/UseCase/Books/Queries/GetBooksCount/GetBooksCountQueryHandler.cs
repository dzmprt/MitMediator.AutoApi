using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.UseCase.Books.Queries.GetBooksByFilter;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBooksCount;

/// <summary>
/// Handler for <see cref="GetBooksQuery"/>.
/// </summary>
/// <param name="booksProvider">Books provider.</param>
internal sealed class GetBooksCountQueryHandler(IBaseProvider<Book> booksProvider) : IRequestHandler<GetBooksCountQuery, int>
{
    /// <inheritdoc/>
    public ValueTask<int> HandleAsync(GetBooksCountQuery request, CancellationToken cancellationToken)
    {
        var freeText = request.FreeText?.Trim().ToUpperInvariant();
        return booksProvider.CountAsync(
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