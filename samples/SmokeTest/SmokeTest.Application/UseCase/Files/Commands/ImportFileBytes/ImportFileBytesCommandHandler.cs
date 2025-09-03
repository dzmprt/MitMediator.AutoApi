using MitMediator;

namespace SmokeTest.Application.UseCase.Files.Commands.ImportFileBytes;

public class ImportFileBytesCommandHandler : IRequestHandler<ImportFileBytesCommand, byte[]>
{
    public async ValueTask<byte[]> HandleAsync(ImportFileBytesCommand request, CancellationToken cancellationToken)
    {
        return await request.ReadToEndAsync(cancellationToken);
    }
}