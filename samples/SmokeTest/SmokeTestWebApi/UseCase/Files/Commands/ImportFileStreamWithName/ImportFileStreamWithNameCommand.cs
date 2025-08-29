using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Files.Commands.ImportFileStreamWithName;

public class ImportFileStreamWithNameCommand : FileRequest, IRequest<FileStreamResponse>
{
}