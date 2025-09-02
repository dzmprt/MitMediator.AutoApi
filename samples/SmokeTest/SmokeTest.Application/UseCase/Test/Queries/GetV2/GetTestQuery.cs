using MitMediator;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Queries.GetV2;

[Version("v2")]
public class GetTestQuery : IRequest<string>
{
    public string? TestData { get; init; }
}