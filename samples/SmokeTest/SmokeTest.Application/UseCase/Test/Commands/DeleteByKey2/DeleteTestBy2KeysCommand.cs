using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.DeleteByKey2;

public class DeleteTestBy2KeysCommand : IRequest, IKeyRequest<int, int>
{
    internal int Key1 { get; private set; }
    
    internal int Key2 { get; private set; }

    public string? TestData { get; init; }

    public void SetKey1(int key)
    {
        Key1 = key;
    }

    public int GetKey1() => Key1;

    public void SetKey2(int key)
    {
        Key2 = key;
    }

    public int GetKey2() => Key2;
}