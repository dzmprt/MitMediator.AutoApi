using MitMediator.AutoApi.Abstractions;

namespace SmokeTestWebApi.UseCase.Test.Commands.CreateBy2Keys;

public class CreateTestBy2KeysResponse : IResourceKey
{
    public string Value { get; set; }
    public string GetResourceKey() => "CreateTestBy2Key";
}