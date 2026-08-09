namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// Key for url.
/// </summary>
/// <typeparam name="TKey">Key.</typeparam>
public interface IKeyRequest<TKey>
{
    TKey Key { get; init; }
}

/// <summary>
/// Keys for url.
/// </summary>
/// <typeparam name="TKey1">Key 1.</typeparam>
/// <typeparam name="TKey2">Key 2.</typeparam>
public interface IKeyRequest<TKey1, TKey2>
{
    TKey1 Key1 { get; init; }
    TKey2 Key2 { get; init; }
}

/// <summary>
/// Keys for url.
/// </summary>
/// <typeparam name="TKey1"></typeparam>
/// <typeparam name="TKey2"></typeparam>
/// <typeparam name="TKey3"></typeparam>
public interface IKeyRequest<TKey1, TKey2, TKey3>
{
    TKey1 Key1 { get; init; }
    TKey2 Key2 { get; init; }
    TKey3 Key3 { get; init; }
}

public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4>
{
    TKey1 Key1 { get; init; }
    TKey2 Key2 { get; init; }
    TKey3 Key3 { get; init; }
    TKey4 Key4 { get; init; }
}

public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
{
    TKey1 Key1 { get; init; }
    TKey2 Key2 { get; init; }
    TKey3 Key3 { get; init; }
    TKey4 Key4 { get; init; }
    TKey5 Key5 { get; init; }
}

public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
{
    TKey1 Key1 { get; init; }
    TKey2 Key2 { get; init; }
    TKey3 Key3 { get; init; }
    TKey4 Key4 { get; init; }
    TKey5 Key5 { get; init; }
    TKey6 Key6 { get; init; }
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
    TKey1 Key1 { get; init; }
    TKey2 Key2 { get; init; }
    TKey3 Key3 { get; init; }
    TKey4 Key4 { get; init; }
    TKey5 Key5 { get; init; }
    TKey6 Key6 { get; init; }
    TKey7 Key7 { get; init; }
}