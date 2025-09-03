using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[Tag("books")]
[Pattern("with-keys/{key1}/field/{key2}")]
[Version("v3")]
[Method(MethodType.Delete)]
public class DoSomeWithBookAndDeleteCommand : IRequest
{
}