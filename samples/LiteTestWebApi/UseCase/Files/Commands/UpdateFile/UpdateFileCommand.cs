using MitMediator;

namespace LiteTestWebApi.UseCase.Files.Commands.UpdateFile;

public class UpdateFileCommand : IRequest<byte[]>
{
    public byte[] Base64String { get; set; }
}