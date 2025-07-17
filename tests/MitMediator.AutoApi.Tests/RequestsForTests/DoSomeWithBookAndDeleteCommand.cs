using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests.RequestsForTests;

[AutoApi("books", customPattern: "with-keys/{key1}/field/{key2}", version: "v3", httpMethodType:HttpMethodType.Delete)]
public class DoSomeWithBookAndDeleteCommand : IRequest
{
    
}