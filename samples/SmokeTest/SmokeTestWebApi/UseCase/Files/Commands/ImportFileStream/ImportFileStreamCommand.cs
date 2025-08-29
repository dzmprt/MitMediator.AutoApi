using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Files.Commands.ImportFileStream;

public class ImportFileStreamCommand : FileRequest, IRequest<Stream>
{
}