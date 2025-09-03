using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Files.Commands.ImportFileBytes;

[DisableAntiforgery]
public class ImportFileBytesCommand : FileRequest, IRequest<byte[]>
{
}