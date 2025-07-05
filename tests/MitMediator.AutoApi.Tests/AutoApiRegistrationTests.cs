using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MitMediator.AutoApi.Abstractions;

namespace MitMediator.AutoApi.Tests;

[Create("create-model", "v1")]
public class CreateCommand : IRequest<string> { }

[Post("post-model", "v1")]
public class PostCommand : IRequest<string> { }

[Update("update-model", "v1")]
public class UpdateCommand : IRequest<string> { }

[UpdateByKey("update-model", "v1")]
public class UpdateByKeyCommand : IRequest<string>, IKeyRequest<int>
{
    public void SetKey(int key) { }
}

[Delete("delete-model", "v1")]
public class DeleteCommand : IRequest<Unit> { }

[DeleteByKey("delete-model", "v1")]
public class DeleteByKeyCommand : IRequest<Unit>, IKeyRequest<int>
{
    public void SetKey(int key) { }
}

[Get("get-model", "v1")]
public class GetCommand : IRequest<string> { }

[GetByKey("get-model", "v1")]
public class GetByKeyCommand : IRequest<string>, IKeyRequest<int>
{
    public void SetKey(int key) { }
}



public class AutoApiRegistrationTests
{
    [Fact]
    public void UseAutoApi_RegistersAllExpectedRoutes()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddMitMediator(typeof(CreateCommand).Assembly);

        var app = builder.Build();
        app.UseAutoApi();
        app.Start();

        var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
        var endpoints = endpointDataSource.Endpoints;

        Assert.Contains(endpoints, e => Matches(e, "POST", "v1/create-model"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "v1/post-model"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/update-model"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/update-model/{key}"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete-model"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete-model/{key}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get-model"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get-model/{key}"));
    }
    
    private static bool Matches(Endpoint endpoint, string verb, string pattern)
    {
        return endpoint is RouteEndpoint re &&
               re.RoutePattern.RawText == pattern &&
               re.Metadata.OfType<HttpMethodMetadata>()
                   .Any(m => m.HttpMethods.Contains(verb));
    }
}
