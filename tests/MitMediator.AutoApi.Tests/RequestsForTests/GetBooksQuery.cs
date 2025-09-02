using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[Tag("my-books")]
[Version("v2")]
[Suffix("favorite")]
public struct GetBooksQuery : IRequest<string>
{

}