namespace MitMediator.AutoApi.Abstractions;

public interface IKeyRequest<in TKey>
{
    void SetKey(TKey key);
}
