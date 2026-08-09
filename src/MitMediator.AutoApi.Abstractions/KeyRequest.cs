namespace MitMediator.AutoApi.Abstractions;

public abstract class KeyRequest<TKey> : IKeyRequest<TKey>
{
    public TKey Key { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2> : IKeyRequest<TKey1, TKey2>
{
    public TKey1 Key1 { get; init; } = default!;
    public TKey2 Key2 { get; init; } = default!;

}

public abstract class KeyRequest<TKey1, TKey2, TKey3> : IKeyRequest<TKey1, TKey2, TKey3>
{
    public TKey1 Key1 { get; init; } = default!;
    public TKey2 Key2 { get; init; } = default!;
    public TKey3 Key3 { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4> : IKeyRequest<TKey1, TKey2, TKey3, TKey4>
{
    public TKey1 Key1 { get; init; } = default!;
    public TKey2 Key2 { get; init; } = default!;
    public TKey3 Key3 { get; init; } = default!;
    public TKey4 Key4 { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5> : IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
{
    public TKey1 Key1 { get; init; } = default!;
    public TKey2 Key2 { get; init; } = default!;
    public TKey3 Key3 { get; init; } = default!;
    public TKey4 Key4 { get; init; } = default!;
    public TKey5 Key5 { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6> : IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
{
    public TKey1 Key1 { get; init; } = default!;
    public TKey2 Key2 { get; init; } = default!;
    public TKey3 Key3 { get; init; } = default!;
    public TKey4 Key4 { get; init; } = default!;
    public TKey5 Key5 { get; init; } = default!;
    public TKey6 Key6 { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7> : IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
{
    public TKey1 Key1 { get; init; } = default!;
    public TKey2 Key2 { get; init; } = default!;
    public TKey3 Key3 { get; init; } = default!;
    public TKey4 Key4 { get; init; } = default!;
    public TKey5 Key5 { get; init; } = default!;
    public TKey6 Key6 { get; init; } = default!;
    public TKey7 Key7 { get; init; } = default!;
}