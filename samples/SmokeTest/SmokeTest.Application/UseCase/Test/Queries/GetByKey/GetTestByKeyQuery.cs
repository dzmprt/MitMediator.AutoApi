using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey;

public class GetTestByKeyQuery : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public void SetKey(int key)
    {
        Key = key;
    }

    public int GetKey() => Key;
}