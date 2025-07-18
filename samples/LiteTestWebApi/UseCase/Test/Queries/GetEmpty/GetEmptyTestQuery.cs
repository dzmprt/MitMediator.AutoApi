using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.GetEmpty;

public class GetEmptyTestQuery : IRequest<string>
{
    public string TestData { get; init; }
}