using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Files.Commands.ImportFileStreamWithName;

[DisableAntiforgery]
public class ImportFileStreamWithNameCommand : FileRequest, IRequest<FileStreamResponse>
{
}