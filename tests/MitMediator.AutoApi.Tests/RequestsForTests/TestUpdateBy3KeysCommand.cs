using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[ExcludeFromCodeCoverage]
[UpdateByKey("PUT", "v1", $"Just {nameof(TestUpdateBy3KeysCommand)} test")]
public class TestUpdateBy3KeysCommand : IRequest<string>, IKeyRequest<int, int, int>
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