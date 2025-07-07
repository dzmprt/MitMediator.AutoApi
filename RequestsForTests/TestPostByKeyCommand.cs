using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests;

[ExcludeFromCodeCoverage]
[PostByKey("POST", "v1", $"Just {nameof(TestPostByKeyCommand)} test")]
public class TestPostByKeyCommand : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }
    
    public string TestData { get; init; }
    
    public void SetKey(int key)
    {
        Key = key;
    }
}