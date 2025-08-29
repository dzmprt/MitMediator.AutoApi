using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Files.Commands.ImportFileWithKey5Command;

[ExcludeFromCodeCoverage]
public class ImportFileWithKey5Command : FileRequest, IRequest<FileStreamResponse>, IKeyRequest<int, int, int, int, int>
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

    public void SetKey4(int key)
    {
 
    }

    public int GetKey4()
    {
        return 0;
    }

    public void SetKey5(int key)
    {
 
    }

    public int GetKey5()
    {
        return 0;
    }
}