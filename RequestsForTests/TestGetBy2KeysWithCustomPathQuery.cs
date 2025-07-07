using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests;

[ExcludeFromCodeCoverage]
[GetByKey("GET", "v1", $"Just {nameof(GetByKeyAttribute)} test with custom puth", "my_custom_path/{key1}/some_field/{key2}/some_part")]
public class TestGetBy2KeysWithCustomPathQuery : IRequest<string>, IKeyRequest<int, int>
{
    internal int Key1 { get; private set; }
    
    internal int Key2 { get; private set; }

    public void SetKey1(int key)
    {
        Key1 = key;
    }

    public void SetKey2(int key)
    {
        Key2 = key;
    }
}