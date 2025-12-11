using MitMediator;

namespace SmokeTest.Application.UseCase.Files.Commands.ImportFileStream;

public class ImportFileStreamCommandHandler : IRequestHandler<ImportFileStreamCommand, Stream>
{
    public async ValueTask<Stream> HandleAsync(ImportFileStreamCommand request, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream();
        await request.GetFileStream().CopyToAsync(stream, cancellationToken);
        return stream;
    }
}