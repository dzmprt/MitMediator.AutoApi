using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Files.Commands.ImportFileStreamWithName;

public class ImportFileStreamWithNameCommandHandler : IRequestHandler<ImportFileStreamWithNameCommand, FileStreamResponse>
{
    public async ValueTask<FileStreamResponse> HandleAsync(ImportFileStreamWithNameCommand request, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream();
        await request.File.CopyToAsync(stream, cancellationToken);
        return new FileStreamResponse(stream, request.FileName) ;
    }
}