using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Files.Queries.GetFileTxt;

[AutoApi(customResponseContentType:"text/plain")]
public class GetFileTxtQuery: IRequest<byte[]>
{
    public string Base64String { get; set; }
}