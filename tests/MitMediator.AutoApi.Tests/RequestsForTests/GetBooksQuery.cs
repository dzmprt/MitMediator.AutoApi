using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[AutoApi("my-books", "v2", PatternSuffix = "favorite")]
public struct GetBooksQuery : IRequest<string>
{

}