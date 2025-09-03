using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Tests.RequestsForTests;
using MitMediator.AutoApi.Tests.RequestsForTests.Test.Queries.Get;

namespace MitMediator.AutoApi.Tests;

public class EndpointsRegistrationsTests
{
    [Fact]
    public void UseAutoApi_RegistersAllExpectedRoutes()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddMitMediator(typeof(GetTestQuery).Assembly);

        var app = builder.Build();
        app.UseAutoApi("api");
        app.Start();

        var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
        var endpoints = endpointDataSource.Endpoints;

        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/tests/with-suffix"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "my_custom_path/{key}/some_field"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "my_custom_path_with_2Keys/{key1}/some_field/{key2}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v2/tests"));

        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/tests/{key}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/tests/{key1}/{key2}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/tests/{key1}/{key2}/{key3}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/tests/{key1}/{key2}/{key3}/{key4}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}"));
        Assert.Contains(endpoints,
            e => Matches(e, "GET", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}"));

        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/v1/tests"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/v1/tests/{key}/by-key"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/v1/tests/{key1}/{key2}/by2-keys"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/v1/tests/{key1}/{key2}/{key3}/by3-keys"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/by4-keys"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/by5-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "PUT", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/by6-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "PUT", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}/by7-keys"));

        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/{key}/by-key"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/by2-keys"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/{key3}/by3-keys"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/by4-keys"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/by5-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/by6-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}/by7-keys"));

        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/create"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/{key}/by-key/create"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/by2-keys/create"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/{key3}/by3-keys/create"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/by4-keys/create"));
        Assert.Contains(endpoints,
            e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/by5-keys/create"));
        Assert.Contains(endpoints,
            e => Matches(e, "POST", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/by6-keys/create"));

        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/v1/tests"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/v1/tests/{key}/by-key"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/v1/tests/{key1}/{key2}/by2-keys"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/v1/tests/{key1}/{key2}/{key3}/by3-keys"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/by4-keys"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/by5-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "DELETE", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/by6-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "DELETE", "api/v1/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}/by7-keys"));
        
        
        Assert.DoesNotContain(endpoints, e => Matches(e, "POST", "TestIgnore"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/custom-tag-but/remove-tag-from-suffix"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/custom-tag-withs/but-remove-tag-from-suffix"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/custom-tag-empty-suffix"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/files/with-custom-name"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/files/stream-with-custom-name"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/files/txt"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/files/png"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/files"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/files/stream"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/files/update"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/files"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/files/{key}/with-key"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/files/{key1}/{key2}/with-key2"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/files/{key1}/{key2}/{key3}/with-key3"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/files/{key1}/{key2}/{key3}/{key4}/with-key4"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/files/{key1}/{key2}/{key3}/{key4}/{key5}/with-key5"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/v1/files/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/with-key6"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/buses/suffix"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/cities"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/heroes"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/potatoes"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/quizzes"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v2/my-books/favorite"));

        Assert.Contains(endpoints, e => Matches(e, "DELETE", "with-keys/{key1}/field/{key2}"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/lists"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v1/nots/supported-prefix"));
    }

    private static bool Matches(Endpoint endpoint, string verb, string pattern)
    {
        return endpoint is RouteEndpoint re &&
               re.RoutePattern.RawText == pattern &&
               re.Metadata.OfType<HttpMethodMetadata>()
                   .Any(m => m.HttpMethods.Contains(verb));
    }
}