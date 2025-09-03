using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace BooksCatalog.Application.UseCase.Books.Commands.UploadBookCover;

[DisableAntiforgery]
public class UploadBookCoverCommand : FileRequest, IKeyRequest<int>, IRequest
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