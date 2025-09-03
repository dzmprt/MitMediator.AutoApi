using MitMediator.AutoApi.Abstractions;

namespace SmokeTest.Application.UseCase.Test.Commands.Create;

public class CreateTestResponse : IResourceKey
{
    public string Value { get; set; }
    public string GetResourceKey() => "CreateTestKey";
}