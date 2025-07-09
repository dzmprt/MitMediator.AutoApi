using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKeyWithCustomPath;

[AutoApi(customPattern: "my_custom_path/{key}/some_field")]
public class GetTestByKeyWithCustomPathQuery : IRequest<string>, IKeyRequest<int>
{
    internal int Key { get; private set; }

    public void SetKey(int key)
    {
        Key = key;
    }
}