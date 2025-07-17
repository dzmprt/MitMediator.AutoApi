using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Files.Queries.GetTxtFile;

[AutoApi(customResponseContentType:"text/plain")]
public class GetFileTxtQuery: IRequest<byte[]>
{
    public string Base64String { get; set; }
}