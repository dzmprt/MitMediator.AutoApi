using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFileWithKey2Command;

[ExcludeFromCodeCoverage]
public class ImportFileWithKey2Command : FileRequest, IRequest<FileStreamResponse>, IKeyRequest<int, int>
{
    public void SetKey1(int key)
    {
 
    }

    public int GetKey1()
    {
        return 0;
    }

    public void SetKey2(int key)
    {
 
    }

    public int GetKey2()
    {
        return 0;
    }
}