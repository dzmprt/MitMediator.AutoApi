using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Files.Commands.ImportFileBytes;

public class ImportFileBytesCommand : FileRequest, IRequest<byte[]>
{
}