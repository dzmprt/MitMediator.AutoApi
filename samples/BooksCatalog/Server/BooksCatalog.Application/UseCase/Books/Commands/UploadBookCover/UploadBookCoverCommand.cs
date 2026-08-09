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
    public int Key { get; init; }
}