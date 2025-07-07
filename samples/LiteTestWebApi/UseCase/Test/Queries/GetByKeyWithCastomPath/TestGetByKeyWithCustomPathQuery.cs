using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKeyWithCastomPath;

[GetByKey("GET", "v1", $"Just {nameof(GetByKeyAttribute)} test with custom puth", "my_custom_path/{key}/some_field")]
public class TestGetByKeyWithCustomPathQuery : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public void SetKey(int key)
    {
        Key = key;
    }
}