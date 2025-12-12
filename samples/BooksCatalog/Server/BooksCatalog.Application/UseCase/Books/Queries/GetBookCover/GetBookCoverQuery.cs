using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace BooksCatalog.Application.UseCase.Books.Queries.GetBookCover;

/// <summary>
/// Get book cover.
/// </summary>
[ResponseContentType("image/png")]
public class GetBookCoverQuery : KeyRequest<int>, IRequest<FileStreamResponse>;