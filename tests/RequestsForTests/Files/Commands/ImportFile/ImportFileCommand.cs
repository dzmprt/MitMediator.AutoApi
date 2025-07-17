using System.Diagnostics.CodeAnalysis;
using MitMediator;

namespace RequestsForTests.Files.Commands.ImportFile;

[ExcludeFromCodeCoverage]
public class ImportFileCommand : IRequest<byte[]>
{
    public byte[] Base64String { get; set; }
}