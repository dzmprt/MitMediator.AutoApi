using BooksCatalog.Application.UseCase.Authors.Commands.DeleteAuthor;
using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Books.Commands.DeleteBook;

/// <summary>
/// Handler for <see cref="DeleteBookCommand"/>.
/// </summary>
/// <param name="booksRepository">Books repository.</param>
internal sealed class DeleteBookCommandHandler(IBaseRepository<Book> booksRepository) : IRequestHandler<DeleteBookCommand>
{
    /// <inheritdoc/>
    public async ValueTask<Unit> HandleAsync(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        var book = await booksRepository.FirstOrDefaultAsync(q => q.BookId == command.GetKey(), cancellationToken);
        if (book is null)
        {
            throw new NotFoundException();
        }
        await booksRepository.RemoveAsync(book, cancellationToken);
        return Unit.Value;
    }
}