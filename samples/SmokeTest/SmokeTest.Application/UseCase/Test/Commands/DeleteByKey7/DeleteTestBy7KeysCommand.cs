using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.DeleteByKey7;

public class DeleteTestBy7KeysCommand : KeyRequest<int, int, int, int, int, int, int>, IRequest
{
    public required string TestData { get; init; }
}