using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Commands.UpdateByKey;

[ResponseContentType("text/plain")]
public class UpdateTestByStringKeyCommand : KeyRequest<string>, IRequest<string>
{
    public required string TestData { get; init; }
}