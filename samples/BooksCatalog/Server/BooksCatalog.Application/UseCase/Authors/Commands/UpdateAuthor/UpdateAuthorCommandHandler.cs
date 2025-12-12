using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Authors.Commands.UpdateAuthor;

/// <summary>
/// Handler for <see cref="UpdateAuthorCommand"/>.
/// </summary>
internal sealed class UpdateAuthorCommandHandler(IBaseRepository<Author> authorRepository)
    : IRequestHandler<UpdateAuthorCommand, Author>
{
    /// <inheritdoc/>
    public async ValueTask<Author> HandleAsync(UpdateAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = await authorRepository.FirstOrDefaultAsync(q => q.AuthorId == command.GetKey(), cancellationToken);
        if (author is null)
        {
            throw new NotFoundException();
        }

        author.UpdateFirstName(command.FirstName);
        author.UpdateLastName(command.LastName);
        await authorRepository.UpdateAsync(author, cancellationToken);
        return author;
    }
}