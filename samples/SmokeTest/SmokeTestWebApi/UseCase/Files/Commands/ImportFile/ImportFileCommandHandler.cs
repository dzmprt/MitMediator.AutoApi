using MitMediator;

namespace SmokeTestWebApi.UseCase.Files.Commands.ImportFile;

public class ImportFileCommandHandler : IRequestHandler<ImportFileCommand, byte[]>
{
    public ValueTask<byte[]> HandleAsync(ImportFileCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(request.Base64String);
    }
}