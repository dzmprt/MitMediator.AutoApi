using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBook;

/// <summary>
/// Handler for <see cref="GetBookQuery"/>.
/// </summary>
/// <param name="booksProvider">Books provider.</param>
internal sealed class GetBookQueryHandler(IBaseProvider<Book> booksProvider) : IRequestHandler<GetBookQuery, Book>
{
    /// <inheritdoc/>
    public async ValueTask<Book> HandleAsync(GetBookQuery query, CancellationToken cancellationToken)
    {
        var book = await booksProvider.FirstOrDefaultAsync(q => q.BookId == query.GetKey(), cancellationToken);
        if (book is null)
        {
            throw new NotFoundException();
        }

        return book;
    }
}