using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.DeleteByKey;

public class DeleteTestByKeyCommand : KeyRequest<int>, IRequest
{
    public string? TestData { get; init; }
}