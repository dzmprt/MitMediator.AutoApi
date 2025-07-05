using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Authors.Commands.DeleteAuthor;

/// <summary>
/// Delete author command.
/// </summary>
[DeleteByKey(nameof(Authors), "v1", "Delete author by id.")]
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
}