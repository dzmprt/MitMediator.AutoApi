using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests;

[AutoApi(customPattern:"TestIgnore", httpMethodType:HttpMethodType.Post)]
[AutoApiIgnore]
public class IgnoreCommand : IRequest
{
}