using MitMediator;
using MitMediator.AutoApi.Abstractions;

namespace RequestsForTests;

[AutoApi("books", customPattern: "with-keys/{key1}/field/{key2}", version: "v3", httpMethodType:HttpMethodType.Delete)]
public class DoSomeWithBookAndDeleteCommand : IRequest
{
    
}