using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Authors.Commands.CreateAuthor;

/// <summary>
/// Handler for <see cref="CreateAuthorCommand"/>.
/// </summary>
internal sealed class CreateAuthorCommandHandler(IBaseRepository<Author> authorRepository) : IRequestHandler<CreateAuthorCommand, CreateAuthorResponse>
{
    /// <inheritdoc/>
    public async ValueTask<CreateAuthorResponse> HandleAsync(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = new Author(command.FirstName, command.LastName);
        await authorRepository.AddAsync(author, cancellationToken);
        return new CreateAuthorResponse(author);
    }
}