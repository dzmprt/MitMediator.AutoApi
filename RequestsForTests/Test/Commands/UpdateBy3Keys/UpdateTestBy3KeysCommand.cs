using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Test.Commands.UpdateBy3Keys;

[ExcludeFromCodeCoverage]
public class UpdateTestBy3KeysCommand : IRequest<string>, IKeyRequest<int, int, int>
{
    internal int Key1 { get; private set; }
    
    internal int Key2 { get; private set; }
    
    internal int Key3 { get; private set; }
    
    public string TestData { get; init; }

    public void SetKey1(int key)
    {
        Key1 = key;
    }

    public void SetKey2(int key)
    {
        Key2 = key;
    }

    public void SetKey3(int key)
    {
        Key3 = key;
    }
}