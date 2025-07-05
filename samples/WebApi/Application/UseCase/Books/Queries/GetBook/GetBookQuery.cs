using Domain;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace Application.UseCase.Books.Queries.GetBook;

/// <summary>
/// Get book query.
/// </summary>
[GetByKey(nameof(Books), "v1", "Get book by id.")]
public struct GetBookQuery : IRequest<Book>, IKeyRequest<int>
{
    /// <summary>
    /// Book id.
    /// </summary>
    internal int BookId { get; private set; }

    public void SetKey(int key)
    {
        BookId = key;
    }
}