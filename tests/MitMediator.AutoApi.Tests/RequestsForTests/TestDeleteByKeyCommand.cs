using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[ExcludeFromCodeCoverage]
[DeleteByKey("DELETE", "v1", $"Just {nameof(DeleteByKeyAttribute)} test")]
public class TestDeleteByKeyCommand : IRequest, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public string TestData { get; init; }
    public void SetKey(int key)
    {
        Key = key;
    }
}