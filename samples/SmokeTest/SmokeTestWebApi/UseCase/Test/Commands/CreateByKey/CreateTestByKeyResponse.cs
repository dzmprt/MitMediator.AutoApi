using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.CreateByKey;

public class CreateTestByKeyResponse : IResourceKey
{
    public string Value { get; set; }
    public string GetResourceKey() => "CreateTestByKey";
}