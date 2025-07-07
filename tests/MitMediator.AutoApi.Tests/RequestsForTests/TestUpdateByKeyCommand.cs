using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[ExcludeFromCodeCoverage]
[UpdateByKey("PUT", "v1", $"Just {nameof(UpdateByKeyAttribute)} test")]
public class TestUpdateByKeyCommand : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }
    
    public string TestData { get; init; }
    
    public void SetKey(int key)
    {
        Key = key;
    }
}