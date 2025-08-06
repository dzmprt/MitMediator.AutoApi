using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Genres.Commands.CreateGenre;

/// <summary>
/// Create author command.
/// </summary>
public struct CreateGenreCommand : IRequest<CreateGenreResponse>
{
    /// <summary>
    /// Genre name.
    /// </summary>
    public string GenreName { get; init; }
}