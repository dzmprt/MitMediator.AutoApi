using MitMediator;

namespace SmokeTestWebApi.UseCase.Files.Commands.UpdateFile;

public class UpdateFileCommand : IRequest<byte[]>
{
    public byte[] Base64String { get; set; }
}