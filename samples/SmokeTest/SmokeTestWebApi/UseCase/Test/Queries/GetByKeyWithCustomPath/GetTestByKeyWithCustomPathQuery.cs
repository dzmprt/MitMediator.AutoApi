using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Queries.GetByKeyWithCustomPath;

[AutoApi(customPattern: "my_custom_path/{key}/some_field")]
public class GetTestByKeyWithCustomPathQuery : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public void SetKey(int key)
    {
        Key = key;
    }

    public int GetKey()
    {
        throw new NotImplementedException();
    }
}