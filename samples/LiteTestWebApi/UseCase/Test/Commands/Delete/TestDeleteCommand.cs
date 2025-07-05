using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace LiteTestWebApi.UseCase.Test.Commands.Delete;

[Delete(nameof(Test), "v1", $"Just {nameof(DeleteAttribute)} test")]
public class TestDeleteCommand : IRequest
{
    public string TestData { get; init; }
}