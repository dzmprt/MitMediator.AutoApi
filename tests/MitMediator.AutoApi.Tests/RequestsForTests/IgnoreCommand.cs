using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[Method(MethodType.Post)]
[Tag("TestIgnore")]
[IgnoreRequest]
public class IgnoreCommand : IRequest
{
}