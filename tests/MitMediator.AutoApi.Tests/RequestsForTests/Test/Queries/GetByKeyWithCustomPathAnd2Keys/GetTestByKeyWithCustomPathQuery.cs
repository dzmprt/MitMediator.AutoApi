using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetByKeyWithCustomPathAnd2Keys;

[ExcludeFromCodeCoverage]
[Pattern("my_custom_path_with_2Keys/{key1}/some_field/{key2}")]
public class GetByKeyWithCustomPathAnd2KeysQuery : IRequest<string>, IKeyRequest<int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
}