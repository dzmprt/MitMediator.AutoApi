using MitMediator;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace SmokeTest.Application.UseCase.Test.Queries.GetByKeyWithCustomPath;


[Pattern("my_custom_path/{key}/some_field")]
public class GetTestByKeyWithCustomPathQuery : KeyRequest<long>, IRequest<string>;