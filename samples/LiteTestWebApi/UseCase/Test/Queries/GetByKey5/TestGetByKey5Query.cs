using LiteTestWebApi.UseCase.Test.Queries.GetByKey4;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.GetByKey5;

[GetByKey("GET", "v1", $"Just {nameof(TestGetByKey5Query)} test")]
public class TestGetByKey5Query : IRequest<string>, IKeyRequest<int, int, int, int, int>
{
    internal int Key1 { get; private set; }
    
    internal int Key2 { get; private set; }
    
    internal int Key3 { get; private set; }
    
    internal int Key4 { get; private set; }
    
    internal int Key5 { get; private set; }
    
    public void SetKey1(int key)
    {
        Key1 = key;
    }

    public void SetKey2(int key)
    {
        Key2 = key;
    }

    public void SetKey3(int key)
    {
        Key3 = key;
    }

    public void SetKey4(int key)
    {
        Key4 = key;
    }

    public void SetKey5(int key)
    {
        Key5 = key;
    }
}