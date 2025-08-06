using MitMediator;

namespace SmokeTestWebApi.UseCase.Files.Commands.UpdateFile;

public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, byte[]>
{
    public ValueTask<byte[]> HandleAsync(UpdateFileCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(request.Base64String);
    }
}