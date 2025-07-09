using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey;

public class GetTestQuery : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public void SetKey(int key)
    {
        Key = key;
    }
}