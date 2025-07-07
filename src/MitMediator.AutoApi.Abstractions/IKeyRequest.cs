namespace MitMediator.AutoApi.Abstractions;

public interface IKeyRequest<in TKey>
{
    void SetKey(TKey key);
}

public interface IKeyRequest<in TKey1, in TKey2>
{
    void SetKey1(TKey1 key);
    
    void SetKey2(TKey2 key);
}

public interface IKeyRequest<in TKey1, in TKey2, in TKey3>
{
    void SetKey1(TKey1 key);
    
    void SetKey2(TKey2 key);
    
    void SetKey3(TKey3 key);
}

public interface IKeyRequest<in TKey1, in TKey2, in TKey3, in TKey4>
{
    void SetKey1(TKey1 key);
    
    void SetKey2(TKey2 key);
    
    void SetKey3(TKey3 key);

    void SetKey4(TKey4 key);
}

public interface IKeyRequest<in TKey1, in TKey2, in TKey3, in TKey4, in TKey5>
{
    void SetKey1(TKey1 key);
    
    void SetKey2(TKey2 key);
    
    void SetKey3(TKey3 key);

    void SetKey4(TKey4 key);
    
    void SetKey5(TKey5 key);
}

public interface IKeyRequest<in TKey1, in TKey2, in TKey3, in TKey4, in TKey5, in TKey6>
{
    void SetKey1(TKey1 key);
    
    void SetKey2(TKey2 key);
    
    void SetKey3(TKey3 key);

    void SetKey4(TKey4 key);
    
    void SetKey5(TKey5 key);
    
    void SetKey6(TKey6 key);
}

public interface IKeyRequest<in TKey1, in TKey2, in TKey3, in TKey4, in TKey5, in TKey6, in TKey7>
{
    void SetKey1(TKey1 key);
    
    void SetKey2(TKey2 key);
    
    void SetKey3(TKey3 key);

    void SetKey4(TKey4 key);
    
    void SetKey5(TKey5 key);
    
    void SetKey6(TKey6 key);
    
    void SetKey7(TKey7 key);
}