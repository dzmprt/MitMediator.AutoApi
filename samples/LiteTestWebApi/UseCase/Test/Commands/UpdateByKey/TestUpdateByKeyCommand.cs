using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateByKey;

[UpdateByKey(nameof(Test), "v1", $"Just {nameof(UpdateByKeyAttribute)} test")]
public class TestUpdateByKeyCommand : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }
    
    public string TestData { get; init; }
    
    public void SetKey(int key)
    {
        Key = key;
    }
}