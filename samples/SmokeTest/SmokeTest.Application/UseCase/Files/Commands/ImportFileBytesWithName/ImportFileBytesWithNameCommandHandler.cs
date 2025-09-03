using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Files.Commands.ImportFileBytesWithName;

public class ImportFileBytesWithNameCommandHandler : IRequestHandler<ImportFileBytesWithNameCommand, FileResponse>
{
    public async ValueTask<FileResponse> HandleAsync(ImportFileBytesWithNameCommand request, CancellationToken cancellationToken)
    {
        var stream = new MemoryStream();
        await request.File.CopyToAsync(stream, cancellationToken);
        return new FileResponse(await request.ReadToEndAsync(cancellationToken), request.FileName) ;
    }
}