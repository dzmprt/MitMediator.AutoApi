using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey7;

[Tag("tests")]
public class GetTestByKey7Query : KeyRequest<int, int, int, int, int, int, int>, IRequest<string>;