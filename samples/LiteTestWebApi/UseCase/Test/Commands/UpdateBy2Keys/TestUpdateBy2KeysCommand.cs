using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.UpdateBy2Keys;

[UpdateByKey("PUT", "v1", $"Just {nameof(TestUpdateBy2KeysCommand)} test")]
public class TestUpdateBy2KeysCommand : IRequest<string>, IKeyRequest<int, int>
{
    internal int Key1 { get; private set; }
    
    internal int Key2 { get; private set; }
    
    public string TestData { get; init; }

    public void SetKey1(int key)
    {
        Key1 = key;
    }

    public void SetKey2(int key)
    {
        Key2 = key;
    }
}