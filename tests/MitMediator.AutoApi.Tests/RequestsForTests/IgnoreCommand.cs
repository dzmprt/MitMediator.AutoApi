using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[AutoApi(customPattern:"TestIgnore", httpMethodType:HttpMethodType.Post)]
[AutoApiIgnore]
public class IgnoreCommand : IRequest
{
}