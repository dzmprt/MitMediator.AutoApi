using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.DeleteByKey7;

public class DeleteTestBy7KeysCommand : IRequest, IKeyRequest<int, int, int, int, int, int, int>
{
    internal int Key1 { get; private set; }
    
    internal int Key2 { get; private set; }
    
    internal int Key3 { get; private set; }
    
    internal int Key4 { get; private set; }
    
    internal int Key5 { get; private set; }
    
    internal int Key6 { get; private set; }
    
    internal int Key7 { get; private set; }

    public string TestData { get; init; }

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

    public void SetKey3(int key)
    {
        Key3 = key;
    }

    public int GetKey3() => Key3;

    public void SetKey4(int key)
    {
        Key4 = key;
    }

    public int GetKey4() => Key4;

    public void SetKey5(int key)
    {
        Key5 = key;
    }

    public int GetKey5() => Key5;

    public void SetKey6(int key)
    {
        Key6 = key;
    }

    public int GetKey6() => Key6;

    public void SetKey7(int key)
    {
        Key7 = key;
    }

    public int GetKey7() => Key7;
}