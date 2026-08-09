using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[Tag("tests")]
public class GetByDateKeysQuery : IRequest<string>, IKeyRequest<DateTime, DateTimeOffset>
{
	public DateTime Key1 { get; init; }
	public DateTimeOffset Key2 { get; init; }
}