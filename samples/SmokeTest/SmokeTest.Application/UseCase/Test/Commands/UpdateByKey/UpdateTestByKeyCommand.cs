using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.UpdateByKey;

public class UpdateTestByKeyCommand : KeyRequest<int>, IRequest<string>
{
    public required string TestData { get; init; }
}