using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Authors.Commands.DeleteAuthor;

/// <summary>
/// Delete author command.
/// </summary>
public class DeleteAuthorCommand : IRequest, IKeyRequest<int>
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