using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.CreateByKey;

[AutoApi(patternSuffix:"create")]
public struct CreateTestByKeyCommand : IRequest<CreateTestByKeyResponse>, IKeyRequest<int>
{
    internal int Key { get; private set; }
    
    public string TestData { get; init; }
    
    public void SetKey(int key)
    {
        Key = key;
    }

    public int GetKey() => Key;
}