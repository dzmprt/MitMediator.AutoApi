using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey2;

public struct GetTestByKey2Query : IKeyRequest<int, int>, IRequest<string>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
}