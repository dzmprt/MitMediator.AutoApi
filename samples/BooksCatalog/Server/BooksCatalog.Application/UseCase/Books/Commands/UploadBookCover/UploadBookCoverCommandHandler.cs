using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Domain;
using BooksCatalog.Domain.Abstractions;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Books.Commands.UploadBookCover;

/// <summary>
/// Handler for <see cref="UploadBookCoverCommand"/>.
/// </summary>
/// <param name="booksRepository">Books repository.</param>
/// <param name="imagesService">Images service.</param>
public class UploadBookCoverCommandHandler(IBaseRepository<Book> booksRepository, IImagesService imagesService) : IRequestHandler<UploadBookCoverCommand, Unit>
{
    public async ValueTask<Unit> HandleAsync(UploadBookCoverCommand request, CancellationToken cancellationToken)
    {
        var book = await booksRepository.FirstOrDefaultAsync(b => b.BookId == request.BookId, cancellationToken);
        if (book is null)
        {
            throw new NotFoundException();
        }

        var imgBytes = await request.ReadToEndAsync(cancellationToken);
        if (!imagesService.IsPngImage(imgBytes))
        {
            throw new BadOperationException("Only PNG images are allowed");
        }
        book.SetCover(imagesService, imgBytes);
        await booksRepository.UpdateAsync(book, cancellationToken);
        return Unit.Value;
    }
}