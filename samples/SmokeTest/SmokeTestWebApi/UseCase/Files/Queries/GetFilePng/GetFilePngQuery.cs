using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Files.Queries.GetFilePng;

[AutoApi(customResponseContentType:"image/png")]
public class GetFilePngQuery: IRequest<byte[]>
{
}