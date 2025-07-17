using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey;

public class DeleteTestByKeyCommand : IRequest, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public string? TestData { get; init; }
    public void SetKey(int key)
    {
        Key = key;
    }

    public int GetKey() => Key;
}