namespace MitMediator.AutoApi.Abstractions;

using System.Text.Json.Serialization;

/// <summary>
/// Key for url.
/// </summary>
/// <typeparam name="TKey">Key.</typeparam>
public interface IKeyRequest<TKey>
{
    [JsonIgnore]
    TKey Key { get; init; }
}

/// <summary>
/// Keys for url.
/// </summary>
/// <typeparam name="TKey1">Key 1.</typeparam>
/// <typeparam name="TKey2">Key 2.</typeparam>
public interface IKeyRequest<TKey1, TKey2>
{
    [JsonIgnore]
    TKey1 Key1 { get; init; }
    [JsonIgnore]
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
    [JsonIgnore]
    TKey1 Key1 { get; init; }
    [JsonIgnore]
    TKey2 Key2 { get; init; }
    [JsonIgnore]
    TKey3 Key3 { get; init; }
}

public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4>
{
    [JsonIgnore]
    TKey1 Key1 { get; init; }
    [JsonIgnore]
    TKey2 Key2 { get; init; }
    [JsonIgnore]
    TKey3 Key3 { get; init; }
    [JsonIgnore]
    TKey4 Key4 { get; init; }
}

public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5>
{
    [JsonIgnore]
    TKey1 Key1 { get; init; }
    [JsonIgnore]
    TKey2 Key2 { get; init; }
    [JsonIgnore]
    TKey3 Key3 { get; init; }
    [JsonIgnore]
    TKey4 Key4 { get; init; }
    [JsonIgnore]
    TKey5 Key5 { get; init; }
}

public interface IKeyRequest<TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
{
    [JsonIgnore]
    TKey1 Key1 { get; init; }
    [JsonIgnore]
    TKey2 Key2 { get; init; }
    [JsonIgnore]
    TKey3 Key3 { get; init; }
    [JsonIgnore]
    TKey4 Key4 { get; init; }
    [JsonIgnore]
    TKey5 Key5 { get; init; }
    [JsonIgnore]
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
    [JsonIgnore]
    TKey1 Key1 { get; init; }
    [JsonIgnore]
    TKey2 Key2 { get; init; }
    [JsonIgnore]
    TKey3 Key3 { get; init; }
    [JsonIgnore]
    TKey4 Key4 { get; init; }
    [JsonIgnore]
    TKey5 Key5 { get; init; }
    [JsonIgnore]
    TKey6 Key6 { get; init; }
    [JsonIgnore]
    TKey7 Key7 { get; init; }
}