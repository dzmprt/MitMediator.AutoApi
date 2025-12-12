using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.PostByKey;

public class PostTestByKeyCommand : KeyRequest<int>, IRequest<string>
{
    public required string TestData { get; init; }
}