using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Books.Commands.CreateBook;

/// <summary>
/// Handler for <see cref="CreateBookCommand"/>
/// </summary>
/// <param name="booksRepository">Books repository.</param>
/// <param name="authorsRepository">Authors repository.</param>
/// <param name="genresRepository">Genres repository.</param>
public class CreateBookCommandHandler(
    IBaseRepository<Book> booksRepository, 
    IBaseRepository<Author> authorsRepository,
    IBaseRepository<Genre> genresRepository) : IRequestHandler<CreateBookCommand, CreatedBookResponse>
{
    /// <inheritdoc/>
    public async ValueTask<CreatedBookResponse> HandleAsync(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var author = await authorsRepository.FirstOrDefaultAsync(a => a.AuthorId == request.AuthorId, cancellationToken);
        if (author is null)
        {
            throw new BadOperationException("Author not found");
        }
        var genre = await genresRepository.FirstOrDefaultAsync(g => g.GenreName == request.GenreName.Trim().ToUpperInvariant(), cancellationToken);
        if (genre is null)
        {
            throw new BadOperationException("Genre not found");
        }
        var book = new Book(request.Title, author, genre);
        await booksRepository.AddAsync(book, cancellationToken);
        return new CreatedBookResponse(book);
    }
}