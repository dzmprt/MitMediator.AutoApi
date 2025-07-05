using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Authors.Queries.GetAuthor;

/// <summary>
/// Get author query.
/// </summary>
[GetByKey(nameof(Authors), "v1", "Get author by id.")]
public struct GetAuthorQuery : IRequest<Author>, IKeyRequest<int>
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