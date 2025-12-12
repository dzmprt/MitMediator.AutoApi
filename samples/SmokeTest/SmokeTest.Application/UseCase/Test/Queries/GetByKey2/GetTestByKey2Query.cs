using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey2;

public class GetTestByKey2Query : KeyRequest<int, int>, IRequest<string>;