using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKeyWithCustomPath;


[Pattern("my_custom_path/{key}/some_field")]
public class GetTestByKeyWithCustomPathQuery : IRequest<string>, IKeyRequest<long>
{
    internal long Key { get; private set; }

    public void SetKey(long key)
    {
        Key = key;
    }

    public long GetKey()
    {
        return Key;
    }
}