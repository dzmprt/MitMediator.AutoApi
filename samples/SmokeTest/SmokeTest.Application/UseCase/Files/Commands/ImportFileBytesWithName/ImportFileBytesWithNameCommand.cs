using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Files.Commands.ImportFileBytesWithName;

[DisableAntiforgery]
public class ImportFileBytesWithNameCommand : FileRequest, IRequest<FileResponse>
{
}