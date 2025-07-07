using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.Create;

[Create("CREATE", "v1", $"Just {nameof(CreateAttribute)} test")]
public class TestCreateCommand : IRequest<string>
{
    public string TestData { get; init; }
}