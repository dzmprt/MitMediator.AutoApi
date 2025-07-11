using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Files.ImportFile;

public class ImportFileCommand : IRequest<string>
{
    public byte[] Base64String { get; set; }
}