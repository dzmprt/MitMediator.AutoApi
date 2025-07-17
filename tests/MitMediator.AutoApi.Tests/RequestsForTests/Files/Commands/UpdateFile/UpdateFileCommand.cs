using System.Diagnostics.CodeAnalysis;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.UpdateFile;

[ExcludeFromCodeCoverage]
public class UpdateFileCommand : IRequest<byte[]>
{
    public byte[] Base64String { get; set; }
}