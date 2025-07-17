using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests;

[AutoApi("my-books", "v2", PatternSuffix = "favorite")]
public struct GetBooksQuery : IRequest<string>
{

}