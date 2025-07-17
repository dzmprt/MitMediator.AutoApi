using MitMediator;

namespace LiteTestWebApi.UseCase.Files.Commands.ImportFile;

public class ImportFileCommand : IRequest<byte[]>
{
    public byte[] Base64String { get; set; }
}