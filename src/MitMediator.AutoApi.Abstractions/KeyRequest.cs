namespace MitMediator.AutoApi.Abstractions;

public abstract class KeyRequest<TKey> : IKeyRequest<TKey>
{
    protected TKey Key;
    public void SetKey(TKey key) => Key = key;
    public TKey GetKey() => Key;
}

public abstract class KeyRequest<TKey1, TKey2> : IKeyRequest<TKey1, TKey2>
{
    protected TKey1 Key1;
    public void SetKey1(TKey1 key) => Key1 = key;
    public TKey1 GetKey1() => Key1;

    protected TKey2 Key2;
    public void SetKey2(TKey2 key) => Key2 = key;
    public TKey2 GetKey2() => Key2;

}

public abstract class KeyRequest<TKey1, TKey2, TKey3> : IKeyRequest<TKey1, TKey2, TKey3>
{
    protected TKey1 Key1;
    public void SetKey1(TKey1 key) => Key1 = key;
    public TKey1 GetKey1() => Key1;

    protected TKey2 Key2;
    public void SetKey2(TKey2 key) => Key2 = key;
    public TKey2 GetKey2() => Key2;
    
    protected TKey3 Key3;
    public void SetKey3(TKey3 key) => Key3 = key;
    public TKey3 GetKey3() => Key3;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4> : IKeyRequest<TKey1, TKey2, TKey3, TKey4>
{
    protected TKey1 Key1;
    public void SetKey1(TKey1 key) => Key1 = key;
    public TKey1 GetKey1() => Key1;

    protected TKey2 Key2;
    public void SetKey2(TKey2 key) => Key2 = key;
    public TKey2 GetKey2() => Key2;
    
    protected TKey3 Key3;
    public void SetKey3(TKey3 key) => Key3 = key;
    public TKey3 GetKey3() => Key3;
    
    protected TKey4 Key4;
    public void SetKey4(TKey4 key) => Key4 = key;
    public TKey4 GetKey4() => Key4;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5> : IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
{
    protected TKey1 Key1;
    public void SetKey1(TKey1 key) => Key1 = key;
    public TKey1 GetKey1() => Key1;

    protected TKey2 Key2;
    public void SetKey2(TKey2 key) => Key2 = key;
    public TKey2 GetKey2() => Key2;
    
    protected TKey3 Key3;
    public void SetKey3(TKey3 key) => Key3 = key;
    public TKey3 GetKey3() => Key3;
    
    protected TKey4 Key4;
    public void SetKey4(TKey4 key) => Key4 = key;
    public TKey4 GetKey4() => Key4;
    
    protected TKey5 Key5;
    public void SetKey5(TKey5 key) => Key5 = key;
    public TKey5 GetKey5() => Key5;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6> : IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
{
    protected TKey1 Key1;
    public void SetKey1(TKey1 key) => Key1 = key;
    public TKey1 GetKey1() => Key1;

    protected TKey2 Key2;
    public void SetKey2(TKey2 key) => Key2 = key;
    public TKey2 GetKey2() => Key2;
    
    protected TKey3 Key3;
    public void SetKey3(TKey3 key) => Key3 = key;
    public TKey3 GetKey3() => Key3;
    
    protected TKey4 Key4;
    public void SetKey4(TKey4 key) => Key4 = key;
    public TKey4 GetKey4() => Key4;
    
    protected TKey5 Key5;
    public void SetKey5(TKey5 key) => Key5 = key;
    public TKey5 GetKey5() => Key5;
    
    protected TKey6 Key6;
    public void SetKey6(TKey6 key) => Key6 = key;
    public TKey6 GetKey6() => Key6;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7> : IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
{
    protected TKey1 Key1;
    public void SetKey1(TKey1 key) => Key1 = key;
    public TKey1 GetKey1() => Key1;

    protected TKey2 Key2;
    public void SetKey2(TKey2 key) => Key2 = key;
    public TKey2 GetKey2() => Key2;
    
    protected TKey3 Key3;
    public void SetKey3(TKey3 key) => Key3 = key;
    public TKey3 GetKey3() => Key3;
    
    protected TKey4 Key4;
    public void SetKey4(TKey4 key) => Key4 = key;
    public TKey4 GetKey4() => Key4;
    
    protected TKey5 Key5;
    public void SetKey5(TKey5 key) => Key5 = key;
    public TKey5 GetKey5() => Key5;
    
    protected TKey6 Key6;
    public void SetKey6(TKey6 key) => Key6 = key;
    public TKey6 GetKey6() => Key6;
    
    protected TKey7 Key7;
    public void SetKey7(TKey7 key) => Key7 = key;
    public TKey7 GetKey7() => Key7;
}