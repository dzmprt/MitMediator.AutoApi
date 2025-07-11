using System.Text;
using MitMediator;

namespace LiteTestWebApi.UseCase.Files.ImportFile;

public class ImportFileCommandHandler : IRequestHandler<ImportFileCommand, string>
{
    public ValueTask<string> HandleAsync(ImportFileCommand request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult($"Result from {nameof(ImportFileCommandHandler)}, file size: {request.Base64String.Length}, UTF8 from base 64: {Encoding.UTF8.GetString(request.Base64String)}");
    }
}