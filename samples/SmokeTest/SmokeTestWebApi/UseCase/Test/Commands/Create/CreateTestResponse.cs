using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.Create;

public class CreateTestResponse : IResourceKey
{
    public string Value { get; set; }
    public string GetResourceKey() => "CreateTestKey";
}