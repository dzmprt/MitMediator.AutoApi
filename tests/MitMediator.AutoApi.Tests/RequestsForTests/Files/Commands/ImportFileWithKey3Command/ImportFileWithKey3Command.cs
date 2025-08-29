using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFileWithKey3Command;

[ExcludeFromCodeCoverage]
public class ImportFileWithKey3Command : FileRequest, IRequest<FileStreamResponse>, IKeyRequest<int, int, int>
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

    public void SetKey3(int key)
    {
 
    }

    public int GetKey3()
    {
        return 0;
    }
}