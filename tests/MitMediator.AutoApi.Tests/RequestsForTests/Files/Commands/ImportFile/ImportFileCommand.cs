using System.Diagnostics.CodeAnalysis;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFile;

[ExcludeFromCodeCoverage]
public class ImportFileCommand : IRequest<byte[]>
{
    public byte[] Base64String { get; set; }
}