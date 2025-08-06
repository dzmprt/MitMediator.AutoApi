using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Genres.Commands.CreateGenre;

public class CreateGenreResponse //: IResourceKey
{
    /// <summary>
    /// Genre name.
    /// </summary>
    public string GenreName { get; init; }

    public string GetResourceKey() => GenreName;
}