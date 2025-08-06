using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Application.UseCase.Authors.Commands.CreateAuthor;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Genres.Commands.CreateGenre;

/// <summary>
/// Handler for <see cref="CreateGenreCommand"/>.
/// </summary>
internal sealed class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, CreateGenreResponse>
{
    private readonly IBaseRepository<Genre> _genreRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateAuthorCommandHandler"/>.
    /// </summary>
    /// <param name="genreRepository">Genre repository.</param>
    public CreateGenreCommandHandler(IBaseRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }
    
    /// <inheritdoc/>
    public async ValueTask<CreateGenreResponse> HandleAsync(CreateGenreCommand command, CancellationToken cancellationToken)
    {
        var isGenreExists = await _genreRepository.AnyAsync(g => g.GenreName == command.GenreName.Trim().ToUpperInvariant(), cancellationToken);
        if (isGenreExists)
        {
            throw new BadOperationException($"Genre '{command.GenreName}' already exists.");
        }
        var genre = new Genre(command.GenreName);
        await _genreRepository.AddAsync(genre, cancellationToken);
        return new CreateGenreResponse
        {
            GenreName = genre.GenreName
        };
    }
}