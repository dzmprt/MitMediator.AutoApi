using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.CreateByKey;

[AutoApi(patternSuffix:"create")]
public struct CreateTestByKeyCommand : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }
    
    public string TestData { get; init; }
    
    public void SetKey(int key)
    {
        Key = key;
    }

    public int GetKey()
    {
        throw new NotImplementedException();
    }
}