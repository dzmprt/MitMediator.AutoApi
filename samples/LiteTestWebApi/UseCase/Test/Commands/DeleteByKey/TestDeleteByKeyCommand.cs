using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey;

[DeleteByKey(nameof(Test), "v1", $"Just {nameof(DeleteByKeyAttribute)} test")]
public class TestDeleteByKeyCommand : IRequest, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public string TestData { get; init; }
    public void SetKey(int key)
    {
        Key = key;
    }
}