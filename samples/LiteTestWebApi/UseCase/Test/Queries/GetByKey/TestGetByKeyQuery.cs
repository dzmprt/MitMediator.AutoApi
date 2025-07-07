using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey;

[GetByKey("GET", "v1", $"Just {nameof(GetByKeyAttribute)} test")]
public class TestGetByKeyQuery : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public void SetKey(int key)
    {
        Key = key;
    }
}