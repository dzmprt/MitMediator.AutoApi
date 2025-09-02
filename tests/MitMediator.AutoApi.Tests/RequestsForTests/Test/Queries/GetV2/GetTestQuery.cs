using System.Diagnostics.CodeAnalysis;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.GetV2;

[ExcludeFromCodeCoverage]
[Version("v2")]
public class GetTestQuery : IRequest<string>
{
}