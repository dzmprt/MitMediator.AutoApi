using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey7WithDifferentTypes;

[Tag("tests")]
public class GetByKey7WithDifferentTypesQuery : KeyRequest<int, string, long, bool, DateTimeOffset, Guid, decimal>, IRequest<string>;