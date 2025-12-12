using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.DeleteByKey2;

public class DeleteTestBy2KeysCommand : KeyRequest<int, int>, IRequest
{
    public string? TestData { get; init; }
}