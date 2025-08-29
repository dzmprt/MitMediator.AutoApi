using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFileWithKeyCommand;

[ExcludeFromCodeCoverage]
public class ImportFileWithKeyCommand : FileRequest, IRequest<FileStreamResponse>, IKeyRequest<int>
{
    public void SetKey(int key)
    {
    }

    public int GetKey()
    {
        return 0;
    }
}