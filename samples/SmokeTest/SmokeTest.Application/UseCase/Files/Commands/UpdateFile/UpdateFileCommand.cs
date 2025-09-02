using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Files.Commands.UpdateFile;


[Method(MethodType.Post)]
[Pattern("api/files/update")]
[DisableAntiforgery]
public class UpdateFileCommand : FileRequest, IRequest<byte[]>
{
}