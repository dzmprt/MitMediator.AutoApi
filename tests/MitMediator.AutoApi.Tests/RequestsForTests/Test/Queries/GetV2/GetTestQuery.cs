using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetV2;

[ExcludeFromCodeCoverage]
[AutoApi(version:"v2")]
public class GetTestQuery : IRequest<string>
{
}