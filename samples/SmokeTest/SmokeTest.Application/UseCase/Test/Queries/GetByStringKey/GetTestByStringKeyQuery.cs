using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKey;

[ResponseContentType("text/plain")]
public class GetTestByStringKeyQuery : KeyRequest<string>, IRequest<string>;