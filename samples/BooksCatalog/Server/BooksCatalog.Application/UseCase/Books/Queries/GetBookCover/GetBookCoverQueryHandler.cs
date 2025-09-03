using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBookCover;

/// <summary>
/// Handler for <see cref="GetBookCoverQuery"/>.
/// </summary>
/// <param name="booksProvider">Books provider.</param>
public class GetBookCoverQueryHandler(IBaseProvider<Book> booksProvider) : IRequestHandler<GetBookCoverQuery, FileStreamResponse>
{
    public async ValueTask<FileStreamResponse> HandleAsync(GetBookCoverQuery request, CancellationToken cancellationToken)
    {
        var book = await booksProvider.FirstOrDefaultAsync(q => q.BookId == request.BookId, cancellationToken);
        if (book is null || book.Cover is null)
        {
            throw new NotFoundException();
        }

        var fileStream = new MemoryStream(book.Cover);
        return new FileStreamResponse(fileStream, $"{book.Title}_cover.png");
    }
}