using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[ExcludeFromCodeCoverage]
[GetByKey("GET", "v1", $"Just {nameof(GetByKeyAttribute)} test with custom puth", "my_custom_path/{key}/some_field")]
public class TestGetByKeyWithCustomPathQuery : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public void SetKey(int key)
    {
        Key = key;
    }
}