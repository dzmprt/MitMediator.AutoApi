using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Queries.Get;

[Get("GET", "v1", $"Just {nameof(GetAttribute)} test")]
public class TestGetQuery : IRequest<string>
{
    public string TestData { get; init; }
}