using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Files.Queries.GetJpgFile;

[AutoApi(customResponseContentType:"image/png")]
public class GetFilePngQuery: IRequest<byte[]>
{
}