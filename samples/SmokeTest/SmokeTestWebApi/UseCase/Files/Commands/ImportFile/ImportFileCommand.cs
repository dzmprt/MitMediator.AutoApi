using MitMediator;

namespace SmokeTestWebApi.UseCase.Files.Commands.ImportFile;

public class ImportFileCommand : IRequest<byte[]>
{
    public byte[] Base64String { get; set; }
}