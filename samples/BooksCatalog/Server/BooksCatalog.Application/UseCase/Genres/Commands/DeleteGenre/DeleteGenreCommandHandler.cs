using BooksCatalog.Application.Abstractions.Infrastructure;
using BooksCatalog.Application.Exceptions;
using BooksCatalog.Domain;
using MitMediator;

namespace BooksCatalog.Application.UseCase.Genres.Commands.DeleteGenre;

/// <summary>
/// Handler for <see cref="DeleteGenreCommand"/>.
/// </summary>
internal sealed class DeleteGenreCommandHandler(IBaseRepository<Genre> genreRepository) : IRequestHandler<DeleteGenreCommand>
{ 
    /// <inheritdoc/>
    public async ValueTask<Unit> HandleAsync(DeleteGenreCommand command, CancellationToken cancellationToken)
    {
        var genreName = command.Key.ToUpperInvariant();
        var genre = await genreRepository.FirstOrDefaultAsync(q => q.GenreName == genreName, cancellationToken);
        if (genre is null)
        {
            throw new NotFoundException();
        }
        await genreRepository.RemoveAsync(genre, cancellationToken);
        return Unit.Value;
    }
}