using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.Update;

[Update(nameof(Test), "v1", $"Just {nameof(CreateAttribute)} test")]
public class TestUpdateCommand : IRequest<string>
{
    public string TestData { get; init; }
}