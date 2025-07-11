using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Test.Commands.CreateBy2Keys;

[ExcludeFromCodeCoverage]
[AutoApi(patternSuffix:"create")]
public class CreateTestBy2KeysCommand : IRequest<string>, IKeyRequest<int, int>
{
    internal int Key1 { get; private set; }
    
    internal int Key2 { get; private set; }
    
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
}