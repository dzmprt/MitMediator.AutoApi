using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Test.Queries.GetByKey;

[ExcludeFromCodeCoverage]
public class GetTestQuery : IRequest<string>, IKeyRequest<int>
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