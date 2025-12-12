using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Authors.Commands.DeleteAuthor;

/// <summary>
/// Handler for <see cref="DeleteAuthorCommand"/>.
/// </summary>
internal sealed class DeleteAuthorCommandHandler(IBaseRepository<Author> authorRepository) : IRequestHandler<DeleteAuthorCommand>
{
    /// <inheritdoc/>
    public async ValueTask<Unit> HandleAsync(DeleteAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = await authorRepository.FirstOrDefaultAsync(q => q.AuthorId == command.GetKey(), cancellationToken);
        if (author is null)
        {
            throw new NotFoundException();
        }
        await authorRepository.RemoveAsync(author, cancellationToken);
        return Unit.Value;
    }
}