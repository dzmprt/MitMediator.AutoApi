using BooksCatalog.Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace BooksCatalog.Application.UseCase.Authors.Queries.GetAuthor;

/// <summary>
/// Get author query.
/// </summary>
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
    
    public int GetKey() => AuthorId;
}