using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Test.Commands.DeleteByKey2;

[ExcludeFromCodeCoverage]
public class DeleteTestBy2KeysCommand : IRequest, IKeyRequest<int, int>
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