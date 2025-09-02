using MitMediator;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Files.Queries.GetFileTxt;

[ResponseContentType("text/plain")]
public class GetFileTxtQuery: IRequest<byte[]>
{
    public string Base64String { get; set; }
}