using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Test.Commands.PostBy4Keys;

[ExcludeFromCodeCoverage]
public class PostTestBy4KeysCommand : IRequest<string>, IKeyRequest<int, int, int, int>
{
    internal int Key1 { get; private set; }
    
    internal int Key2 { get; private set; }
    
    internal int Key3 { get; private set; }
    
    internal int Key4 { get; private set; }
    
    public string TestData { get; init; }

    public void SetKey1(int key)
    {
        Key1 = key;
    }

    public int GetKey1()
    {
        throw new NotImplementedException();
    }

    public void SetKey2(int key)
    {
        Key2 = key;
    }

    public int GetKey2()
    {
        throw new NotImplementedException();
    }

    public void SetKey3(int key)
    {
        Key3 = key;
    }

    public int GetKey3()
    {
        throw new NotImplementedException();
    }

    public void SetKey4(int key)
    {
        Key4 = key;
    }

    public int GetKey4()
    {
        throw new NotImplementedException();
    }
}