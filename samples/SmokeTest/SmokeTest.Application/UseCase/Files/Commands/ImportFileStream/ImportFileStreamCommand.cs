using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Files.Commands.ImportFileStream;

[DisableAntiforgery]
public class ImportFileStreamCommand : FileRequest, IRequest<Stream>
{
}