using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBookCover;

/// <summary>
/// Get book cover.
/// </summary>
[ResponseContentType("image/png")]
public struct GetBookCoverQuery : IRequest<FileStreamResponse>, IKeyRequest<int>
{
    /// <summary>
    /// Book id.
    /// </summary>
    internal int BookId { get; private set; }

    public void SetKey(int key)
    {
        BookId = key;
    }
    
    public int GetKey() => BookId;
}