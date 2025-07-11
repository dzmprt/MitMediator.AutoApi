namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// Key for url.
/// </summary>
/// <typeparam name="TKey">Key.</typeparam>
public interface IKeyRequest<TKey>
{
    /// <summary>
    /// Set key.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey(TKey key);
    
    /// <summary>
    /// Get key.
    /// </summary>
    /// <returns>Key.</returns>
    TKey GetKey();
}

/// <summary>
/// Keys for url.
/// </summary>
/// <typeparam name="TKey1">Key 1.</typeparam>
/// <typeparam name="TKey2">Key 2.</typeparam>
public interface IKeyRequest<TKey1, TKey2>
{
    /// <summary>
    /// Set key 1.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey1(TKey1 key);
    
    /// <summary>
    /// Get key 1.
    /// </summary>
    /// <returns>Key.</returns>
    TKey1 GetKey1();

    /// <summary>
    /// Set key 2.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey2(TKey2 key);
    
    /// <summary>
    /// Get key 2.
    /// </summary>
    /// <returns>Key.</returns>
    TKey2 GetKey2();
}

/// <summary>
/// Keys for url.
/// </summary>
/// <typeparam name="TKey1"></typeparam>
/// <typeparam name="TKey2"></typeparam>
/// <typeparam name="TKey3"></typeparam>
public interface IKeyRequest<TKey1, TKey2, TKey3>
{
    /// <summary>
    /// Set key 1.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey1(TKey1 key);
    
    /// <summary>
    /// Get key 1.
    /// </summary>
    /// <returns>Key.</returns>
    TKey1 GetKey1();

    /// <summary>
    /// Set key 2.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey2(TKey2 key);
    
    /// <summary>
    /// Get key 2.
    /// </summary>
    /// <returns>Key.</returns>
    TKey2 GetKey2();

    /// <summary>
    /// Set key 3.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey3(TKey3 key);
    
    /// <summary>
    /// Get key 3.
    /// </summary>
    /// <returns>Key.</returns>
    TKey3 GetKey3();
}

public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4>
{
    /// <summary>
    /// Set key 1.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey1(TKey1 key);
    
    /// <summary>
    /// Get key 1.
    /// </summary>
    /// <returns>Key.</returns>
    TKey1 GetKey1();

    /// <summary>
    /// Set key 2.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey2(TKey2 key);
    
    /// <summary>
    /// Get key 2.
    /// </summary>
    /// <returns>Key.</returns>
    TKey2 GetKey2();

    /// <summary>
    /// Set key 3.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey3(TKey3 key);
    
    /// <summary>
    /// Get key 3.
    /// </summary>
    /// <returns>Key.</returns>
    TKey3 GetKey3();

    /// <summary>
    /// Set key 4.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey4(TKey4 key);
    
    /// <summary>
    /// Get key 4.
    /// </summary>
    /// <returns>Key.</returns>
    TKey4 GetKey4();
}

public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
{
    /// <summary>
    /// Set key 1.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey1(TKey1 key);
    
    /// <summary>
    /// Get key 1.
    /// </summary>
    /// <returns>Key.</returns>
    TKey1 GetKey1();

    /// <summary>
    /// Set key 2.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey2(TKey2 key);
    
    /// <summary>
    /// Get key 2.
    /// </summary>
    /// <returns>Key.</returns>
    TKey2 GetKey2();

    /// <summary>
    /// Set key 3.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey3(TKey3 key);
    
    /// <summary>
    /// Get key 3.
    /// </summary>
    /// <returns>Key.</returns>
    TKey3 GetKey3();

    /// <summary>
    /// Set key 4.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey4(TKey4 key);
    
    /// <summary>
    /// Get key 4.
    /// </summary>
    /// <returns>Key.</returns>
    TKey4 GetKey4();

    /// <summary>
    /// Set key 5.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey5(TKey5 key);
    
    /// <summary>
    /// Get key 5.
    /// </summary>
    /// <returns>Key.</returns>
    TKey5 GetKey5();
}

public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
{
    /// <summary>
    /// Set key 1.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey1(TKey1 key);
    
    /// <summary>
    /// Get key 1.
    /// </summary>
    /// <returns>Key.</returns>
    TKey1 GetKey1();

    /// <summary>
    /// Set key 2.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey2(TKey2 key);
    
    /// <summary>
    /// Get key 2.
    /// </summary>
    /// <returns>Key.</returns>
    TKey2 GetKey2();

    /// <summary>
    /// Set key 3.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey3(TKey3 key);
    
    /// <summary>
    /// Get key 3.
    /// </summary>
    /// <returns>Key.</returns>
    TKey3 GetKey3();

    /// <summary>
    /// Set key 4.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey4(TKey4 key);
    
    /// <summary>
    /// Get key 4.
    /// </summary>
    /// <returns>Key.</returns>
    TKey4 GetKey4();

    /// <summary>
    /// Set key 5.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey5(TKey5 key);
    
    /// <summary>
    /// Get key 5.
    /// </summary>
    /// <returns>Key.</returns>
    TKey5 GetKey5();

    /// <summary>
    /// Set key 6.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey6(TKey6 key);
    
    /// <summary>
    /// Get key 6.
    /// </summary>
    /// <returns>Key.</returns>
    TKey6 GetKey6();
}

/// <summary>
/// Keys for url.
/// </summary>
/// <typeparam name="TKey1">Key1 </typeparam>
/// <typeparam name="TKey2">Key 2</typeparam>
/// <typeparam name="TKey3">Key 3</typeparam>
/// <typeparam name="TKey4">Key 4</typeparam>
/// <typeparam name="TKey5">Key 5</typeparam>
/// <typeparam name="TKey6">Key 6</typeparam>
/// <typeparam name="TKey7">Key 7</typeparam>
public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
{
    /// <summary>
    /// Set key 1.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey1(TKey1 key);
    
    /// <summary>
    /// Get key 1.
    /// </summary>
    /// <returns>Key.</returns>
    TKey1 GetKey1();

    /// <summary>
    /// Set key 2.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey2(TKey2 key);
    
    /// <summary>
    /// Get key 2.
    /// </summary>
    /// <returns>Key.</returns>
    TKey2 GetKey2();

    /// <summary>
    /// Set key 3.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey3(TKey3 key);
    
    /// <summary>
    /// Get key 3.
    /// </summary>
    /// <returns>Key.</returns>
    TKey3 GetKey3();

    /// <summary>
    /// Set key 4.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey4(TKey4 key);
    
    /// <summary>
    /// Get key 4.
    /// </summary>
    /// <returns>Key.</returns>
    TKey4 GetKey4();

    /// <summary>
    /// Set key 5.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey5(TKey5 key);
    
    /// <summary>
    /// Get key 5.
    /// </summary>
    /// <returns>Key.</returns>
    TKey5 GetKey5();

    /// <summary>
    /// Set key 6.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey6(TKey6 key);
    
    /// <summary>
    /// Get key 6.
    /// </summary>
    /// <returns>Key.</returns>
    TKey6 GetKey6();

    /// <summary>
    /// Set key 7.
    /// </summary>
    /// <param name="key">Key.</param>
    void SetKey7(TKey7 key);
    
    /// <summary>
    /// Get key 7.
    /// </summary>
    /// <returns>Key.</returns>
    TKey7 GetKey7();
}