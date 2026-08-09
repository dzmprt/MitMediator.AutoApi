using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[Tag("tests")]
public class GetByKey7WithDifferentTypesQuery : IRequest<string>, IKeyRequest<int, string, long, bool, DateTime, Guid, decimal>
{
    public int Key1 { get; init; }
    public string Key2 { get; init; }
    public long Key3 { get; init; }
    public bool Key4 { get; init; }
    public DateTime Key5 { get; init; }
    public Guid Key6 { get; init; }
    public decimal Key7 { get; init; }
}