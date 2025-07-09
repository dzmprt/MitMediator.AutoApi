using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Genres.Commands.DeleteGenre;

/// <summary>
/// Delete genre command.
/// </summary>
public struct DeleteGenreCommand : IRequest, IKeyRequest<string>
{
    /// <summary>
    /// Genre name.
    /// </summary>
    internal string GenreName { get; private set; }

    public void SetKey(string key)
    {
        GenreName = key;
    }
}