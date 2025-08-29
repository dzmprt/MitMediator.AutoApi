using MitMediator;

namespace SmokeTestWebApi.UseCase.Files.Commands.ImportFileStream;

public class ImportFileStreamCommandHandler : IRequestHandler<ImportFileStreamCommand, Stream>
{
    public async ValueTask<Stream> HandleAsync(ImportFileStreamCommand request, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream();
        await request.File.CopyToAsync(stream, cancellationToken);
        return stream;
    }
}