using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.CreateBy6Keys;

public class CreateTestBy6KeysResponse : IResourceKey
{
    public string Value { get; set; }
    public string GetResourceKey() => "CreateTestBy6Key";
}