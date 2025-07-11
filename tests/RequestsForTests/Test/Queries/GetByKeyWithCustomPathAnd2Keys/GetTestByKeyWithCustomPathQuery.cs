using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Test.Queries.GetByKeyWithCustomPathAnd2Keys;

[ExcludeFromCodeCoverage]
[AutoApi(customPattern: "my_custom_path_with_2Keys/{key1}/some_field/{key2}")]
public class GetByKeyWithCustomPathAnd2KeysQuery : IRequest<string>, IKeyRequest<int, int>
{
    internal int Key1 { get; private set; }
    
    internal int Key2 { get; private set; }

    public void SetKey1(int key)
    {
        Key1 = key;
    }

    public int GetKey1()
    {
        throw new NotImplementedException();
    }

    public void SetKey2(int key)
    {
        Key2 = key;
    }

    public int GetKey2()
    {
        throw new NotImplementedException();
    }
}