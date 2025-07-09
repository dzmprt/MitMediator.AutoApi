using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.Get;

public class GetTestQuery : IRequest<string>
{
    public string TestData { get; init; }
}