using MitMediator;

namespace SmokeTest.Application.UseCase.Files.Commands.UpdateFile;

public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, byte[]>
{
    public async ValueTask<byte[]> HandleAsync(UpdateFileCommand request, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream();
        await request.File.CopyToAsync(stream, cancellationToken);
        return stream.ToArray();
    }
}