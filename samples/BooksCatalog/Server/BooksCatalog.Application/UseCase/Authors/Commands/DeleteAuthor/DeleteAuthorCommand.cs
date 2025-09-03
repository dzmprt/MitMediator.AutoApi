using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Authors.Commands.DeleteAuthor;

/// <summary>
/// Delete author command.
/// </summary>
public struct DeleteAuthorCommand : IRequest, IKeyRequest<int>
{
    /// <summary>
    /// Author id.
    /// </summary>
    internal int AuthorId { get; private set; }

    public void SetKey(int key)
    {
        AuthorId = key;
    }

    public int GetKey() => AuthorId;
}