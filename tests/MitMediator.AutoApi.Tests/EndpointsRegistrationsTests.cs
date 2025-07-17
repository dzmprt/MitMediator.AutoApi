using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        Assert.Contains(endpoints, e => Matches(e, "GET", "api/tests/with-suffix"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "my_custom_path/{key}/some_field"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "my_custom_path_with_2Keys/{key1}/some_field/{key2}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v2/tests"));

        Assert.Contains(endpoints, e => Matches(e, "GET", "api/tests/{key}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/tests/{key1}/{key2}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/tests/{key1}/{key2}/{key3}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/tests/{key1}/{key2}/{key3}/{key4}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}"));
        Assert.Contains(endpoints,
            e => Matches(e, "GET", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}"));

        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/tests"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/tests/{key}/by-key"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/tests/{key1}/{key2}/by2-keys"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/tests/{key1}/{key2}/{key3}/by3-keys"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/tests/{key1}/{key2}/{key3}/{key4}/by4-keys"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/by5-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "PUT", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/by6-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "PUT", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}/by7-keys"));

        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/{key}/by-key"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/{key1}/{key2}/by2-keys"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/{key1}/{key2}/{key3}/by3-keys"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/{key1}/{key2}/{key3}/{key4}/by4-keys"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/by5-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "POST", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/by6-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "POST", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}/by7-keys"));

        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/create"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/{key}/by-key/create"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/{key1}/{key2}/by2-keys/create"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/{key1}/{key2}/{key3}/by3-keys/create"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/tests/{key1}/{key2}/{key3}/{key4}/by4-keys/create"));
        Assert.Contains(endpoints,
            e => Matches(e, "POST", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/by5-keys/create"));
        Assert.Contains(endpoints,
            e => Matches(e, "POST", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/by6-keys/create"));

        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/tests"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/tests/{key}/by-key"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/tests/{key1}/{key2}/by2-keys"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/tests/{key1}/{key2}/{key3}/by3-keys"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/tests/{key1}/{key2}/{key3}/{key4}/by4-keys"));
        Assert.Contains(endpoints, e => Matches(e, "DELETE", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/by5-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "DELETE", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/by6-keys"));
        Assert.Contains(endpoints,
            e => Matches(e, "DELETE", "api/tests/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}/by7-keys"));
        
        
        Assert.DoesNotContain(endpoints, e => Matches(e, "POST", "TestIgnore"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/custom-tag-but/remove-tag-from-suffix"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/custom-tag-withs/but-remove-tag-from-suffix"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/custom-tag-empty-suffix"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/files/with-custom-name"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/files/txt"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/files/png"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/files"));
        Assert.Contains(endpoints, e => Matches(e, "PUT", "api/files"));
        Assert.Contains(endpoints, e => Matches(e, "POST", "api/files"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/buses/suffix"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/cities"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/heroes"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/potatoes"));
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/quizzes"));
        
        Assert.Contains(endpoints, e => Matches(e, "GET", "api/v2/my-books/favorite"));

        Assert.Contains(endpoints, e => Matches(e, "DELETE", "with-keys/{key1}/field/{key2}"));

    }

    private static bool Matches(Endpoint endpoint, string verb, string pattern)
    {
        return endpoint is RouteEndpoint re &&
               re.RoutePattern.RawText == pattern &&
               re.Metadata.OfType<HttpMethodMetadata>()
                   .Any(m => m.HttpMethods.Contains(verb));
    }
}