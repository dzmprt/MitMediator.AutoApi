using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Application.UseCase.Authors.Commands.CreateAuthor;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Genres.Commands.CreateGenre;

/// <summary>
/// Handler for <see cref="CreateGenreCommand"/>.
/// </summary>
/// <param name="genreRepository">Genre repository.</param>
internal sealed class CreateGenreCommandHandler(IBaseRepository<Genre> genreRepository) : IRequestHandler<CreateGenreCommand, CreateGenreResponse>
{
    /// <inheritdoc/>
    public async ValueTask<CreateGenreResponse> HandleAsync(CreateGenreCommand command, CancellationToken cancellationToken)
    {
        var isGenreExists = await genreRepository.AnyAsync(g => g.GenreName == command.GenreName.Trim().ToUpperInvariant(), cancellationToken);
        if (isGenreExists)
        {
            throw new BadOperationException($"Genre '{command.GenreName}' already exists.");
        }
        var genre = new Genre(command.GenreName);
        await genreRepository.AddAsync(genre, cancellationToken);
        return new CreateGenreResponse
        {
            GenreName = genre.GenreName
        };
    }
}