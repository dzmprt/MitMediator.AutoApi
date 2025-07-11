using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Test.Commands.DeleteByKey;

[ExcludeFromCodeCoverage]
public class DeleteTestByKeyCommand : IRequest, IKeyRequest<int>
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