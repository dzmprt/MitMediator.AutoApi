using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetByKeyWithCustomPath;

[ExcludeFromCodeCoverage]
[Pattern("my_custom_path/{key}/some_field")]
public class GetTestByKeyWithCustomPathQuery : IRequest<string>, IKeyRequest<int>
{
    public int Key { get; init; }
}