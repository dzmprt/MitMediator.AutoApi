namespace MitMediator.AutoApi.Abstractions;

using System.Text.Json.Serialization;

public abstract class KeyRequest<TKey> : IKeyRequest<TKey>
{
    [JsonIgnore]
    public TKey Key { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2> : IKeyRequest<TKey1, TKey2>
{
    [JsonIgnore]
    public TKey1 Key1 { get; init; } = default!;
    [JsonIgnore]
    public TKey2 Key2 { get; init; } = default!;

}

public abstract class KeyRequest<TKey1, TKey2, TKey3> : IKeyRequest<TKey1, TKey2, TKey3>
{
    [JsonIgnore]
    public TKey1 Key1 { get; init; } = default!;
    [JsonIgnore]
    public TKey2 Key2 { get; init; } = default!;
    [JsonIgnore]
    public TKey3 Key3 { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4> : IKeyRequest<TKey1, TKey2, TKey3, TKey4>
{
    [JsonIgnore]
    public TKey1 Key1 { get; init; } = default!;
    [JsonIgnore]
    public TKey2 Key2 { get; init; } = default!;
    [JsonIgnore]
    public TKey3 Key3 { get; init; } = default!;
    [JsonIgnore]
    public TKey4 Key4 { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5> : IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
{
    [JsonIgnore]
    public TKey1 Key1 { get; init; } = default!;
    [JsonIgnore]
    public TKey2 Key2 { get; init; } = default!;
    [JsonIgnore]
    public TKey3 Key3 { get; init; } = default!;
    [JsonIgnore]
    public TKey4 Key4 { get; init; } = default!;
    [JsonIgnore]
    public TKey5 Key5 { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6> : IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
{
    [JsonIgnore]
    public TKey1 Key1 { get; init; } = default!;
    [JsonIgnore]
    public TKey2 Key2 { get; init; } = default!;
    [JsonIgnore]
    public TKey3 Key3 { get; init; } = default!;
    [JsonIgnore]
    public TKey4 Key4 { get; init; } = default!;
    [JsonIgnore]
    public TKey5 Key5 { get; init; } = default!;
    [JsonIgnore]
    public TKey6 Key6 { get; init; } = default!;
}

public abstract class KeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7> : IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
{
    [JsonIgnore]
    public TKey1 Key1 { get; init; } = default!;
    [JsonIgnore]
    public TKey2 Key2 { get; init; } = default!;
    [JsonIgnore]
    public TKey3 Key3 { get; init; } = default!;
    [JsonIgnore]
    public TKey4 Key4 { get; init; } = default!;
    [JsonIgnore]
    public TKey5 Key5 { get; init; } = default!;
    [JsonIgnore]
    public TKey6 Key6 { get; init; } = default!;
    [JsonIgnore]
    public TKey7 Key7 { get; init; } = default!;
}