using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.GetV2;

[AutoApi(version:"v2")]
public class GetTestQuery : IRequest<string>
{
    public string TestData { get; init; }
}