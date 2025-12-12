using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey;

public class GetTestByKeyQuery : KeyRequest<int>, IRequest<string>;