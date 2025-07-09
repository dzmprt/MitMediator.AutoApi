using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Genres.Commands.CreateGenre;

/// <summary>
/// Create author command.
/// </summary>
public struct CreateGenreCommand : IRequest<Genre>
{
    /// <summary>
    /// Genre name.
    /// </summary>
    public string GenreName { get; init; }
}