using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Tests.RequestsForTests;

namespace MitMediator.AutoApi.Tests;

public class EndpointsRegistrationsTests
{
    [Fact]
    public void UseAutoApi_RegistersAllExpectedRoutes()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddMitMediator(typeof(TestUpdateCommand).Assembly);

        var app = builder.Build();
        app.UseAutoApi();
        app.Start();

        var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
        var endpoints = endpointDataSource.Endpoints;

        Assert.Contains(endpoints, e => Matches(e, "POST", "v1/create"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "v1/create/{key}"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "v1/create/{key1}/{key2}"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "v1/create/{key1}/{key2}/{key3}"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "v1/create/{key1}/{key2}/{key3}/{key4}"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "v1/create/{key1}/{key2}/{key3}/{key4}/{key5}"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "v1/create/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}"));
        
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete/{key}"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete/{key1}/{key2}"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete/{key1}/{key2}/{key3}"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete/{key1}/{key2}/{key3}/{key4}"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete/{key1}/{key2}/{key3}/{key4}/{key5}"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "v1/delete/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "get-model"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get/{key}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get/{key1}/{key2}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get/{key1}/{key2}/{key3}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get/{key1}/{key2}/{key3}/{key4}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get/{key1}/{key2}/{key3}/{key4}/{key5}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "v1/get/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "my_custom_path/{key}/some_field"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "my_custom_path/{key1}/some_field/{key2}/some_part"));
        
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/put"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/put/{key}"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/put/{key1}/{key2}"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/put/{key1}/{key2}/{key3}"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/put/{key1}/{key2}/{key3}/{key4}"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/put/{key1}/{key2}/{key3}/{key4}/{key5}"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/put/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "v1/put/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "v2/get-model"));
    }

    private static bool Matches(Endpoint endpoint, string verb, string pattern)
    {
        return endpoint is RouteEndpoint re &&
               re.RoutePattern.RawText == pattern &&
               re.Metadata.OfType<HttpMethodMetadata>()
                   .Any(m => m.HttpMethods.Contains(verb));
    }
}
