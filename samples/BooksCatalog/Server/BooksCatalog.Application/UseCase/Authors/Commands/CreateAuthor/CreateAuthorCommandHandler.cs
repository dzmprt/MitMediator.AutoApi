using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Authors.Commands.CreateAuthor;

/// <summary>
/// Handler for <see cref="CreateAuthorCommand"/>.
/// </summary>
internal sealed class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreateAuthorResponse>
{
    private readonly IBaseRepository<Author> _authorRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAuthorCommandHandler"/>.
    /// </summary>
    /// <param name="authorRepository">Author repository.</param>
    public CreateAuthorCommandHandler(IBaseRepository<Author> authorRepository)
    {
        _authorRepository = authorRepository;
    }
    
    /// <inheritdoc/>
    public async ValueTask<CreateAuthorResponse> HandleAsync(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        var author = new Author(command.FirstName, command.LastName);
        await _authorRepository.AddAsync(author, cancellationToken);
        return new CreateAuthorResponse(author);
    }
}