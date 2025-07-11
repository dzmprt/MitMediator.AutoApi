using System.Diagnostics.CodeAnalysis;
using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests.Test.Queries.GetV2;

[ExcludeFromCodeCoverage]
[AutoApi(version:"v2")]
public class GetTestQuery : IRequest<string>
{
}