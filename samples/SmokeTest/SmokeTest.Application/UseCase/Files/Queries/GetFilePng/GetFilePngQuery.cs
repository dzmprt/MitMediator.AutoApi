using MitMediator;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Files.Queries.GetFilePng;

[ResponseContentType("image/png")]
public class GetFilePngQuery: IRequest<byte[]>
{
}