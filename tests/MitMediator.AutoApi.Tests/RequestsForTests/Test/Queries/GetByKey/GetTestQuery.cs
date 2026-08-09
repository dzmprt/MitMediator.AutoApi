using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetByKey;

[ExcludeFromCodeCoverage]
public class GetTestQuery : IRequest<string>, IKeyRequest<int>
{
    public int Key { get; init; }
}