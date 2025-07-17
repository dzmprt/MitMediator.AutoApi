using System.Diagnostics.CodeAnalysis;
using MitMediator;

namespace RequestsForTests.Files.Commands.UpdateFile;

[ExcludeFromCodeCoverage]
public class UpdateFileCommand : IRequest<byte[]>
{
    public byte[] Base64String { get; set; }
}