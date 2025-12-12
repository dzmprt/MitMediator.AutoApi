using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[Tag("tests")]
public class GetByKey7WithDifferentTypesQuery : IRequest<string>, IKeyRequest<int, string, long, bool, DateTime, Guid, decimal>
{
    internal int Key1 { get; private set; }
    
    internal string Key2 { get; private set; }
    
    internal long Key3 { get; private set; }
    
    internal bool Key4 { get; private set; }
    
    internal DateTime Key5 { get; private set; }
    
    internal Guid Key6 { get; private set; }
    
    internal decimal Key7 { get; private set; }
    
    public void SetKey1(int key)
    {
        Key1 = key;
    }

    public int GetKey1() => Key1;

    public void SetKey2(string key)
    {
        Key2 = key;
    }

    public string GetKey2() => Key2;

    public void SetKey3(long key)
    {
        Key3 = key;
    }

    public long GetKey3() => Key3;

    public void SetKey4(bool key)
    {
        Key4 = key;
    }

    public bool GetKey4() => Key4;

    public void SetKey5(DateTime key)
    {
        Key5 = key;
    }

    public DateTime GetKey5() => Key5;

    public void SetKey6(Guid key)
    {
        Key6 = key;
    }

    public Guid GetKey6() => Key6;

    public void SetKey7(decimal key)
    {
       Key7 = key;
    }

    public decimal GetKey7() => Key7;
}