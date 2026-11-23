using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;

namespace MitMediator.AutoApi.Tests;

public class AutomaticEndpointGenerationTests
{
    public static TheoryData<string, int, bool> RequestMatrix()
    {
        var data = new TheoryData<string, int, bool>();
        foreach (var prefix in new[] { "Get", "Update", "Post", "Create", "Delete" })
        {
            for (var keysCount = 0; keysCount <= 7; keysCount++)
            {
                data.Add(prefix, keysCount, false);
                data.Add(prefix, keysCount, true);
            }
        }

        return data;
    }

    public static TheoryData<int, bool> FileRequestMatrix()
    {
        var data = new TheoryData<int, bool>();
        for (var keysCount = 0; keysCount <= 7; keysCount++)
        {
            data.Add(keysCount, false);
            data.Add(keysCount, true);
        }

        return data;
    }

    public static TheoryData<Type, string> RouteConstraintMatrix()
    {
        return new TheoryData<Type, string>
        {
            { typeof(int), "{key:int}" },
            { typeof(long), "{key:long}" },
            { typeof(bool), "{key:bool}" },
            { typeof(DateTime), "{key:datetime}" },
            { typeof(DateTimeOffset), "{key:datetime}" },
            { typeof(decimal), "{key:decimal}" },
            { typeof(double), "{key:double}" },
            { typeof(float), "{key:float}" },
            { typeof(Guid), "{key:guid}" },
            { typeof(string), "{key}" }
        };
    }

    [Theory]
    [MemberData(nameof(RequestMatrix))]
    public async Task UseAutoApi_GeneratesEveryMethodAndKeyCount(
        string prefix,
        int keysCount,
        bool taskHandler)
    {
        var requestType = TypeFactory.CreateApiRequestType(
            $"{prefix}MatrixRequest",
            keysCount);
        await using var app = BuildApp(requestType, taskHandler);
        var requestInfos = RequestHelper.GetRequestsInfos([requestType.Assembly], "api").ToArray();
        Assert.Contains(requestInfos, info => info.RequestType == requestType);

        app.UseAutoApi("api", [requestType.Assembly]);
        await app.StartAsync();

        var endpoint = Assert.Single(
            app.Services.GetRequiredService<EndpointDataSource>().Endpoints.OfType<RouteEndpoint>(),
            candidate => candidate.RoutePattern.RawText!.StartsWith("api/", StringComparison.Ordinal));

        Assert.Equal(GetExpectedHttpMethod(prefix), GetHttpMethod(endpoint));
        Assert.Equal(GetExpectedPattern(keysCount), endpoint.RoutePattern.RawText);
    }

    [Theory]
    [MemberData(nameof(FileRequestMatrix))]
    public async Task UseAutoApi_GeneratesMultipartRequestForEveryKeyCount(
        int keysCount,
        bool taskHandler)
    {
        var requestType = GetFileRequestType(keysCount);
        await using var app = BuildApp(requestType, taskHandler);

        app.UseAutoApi("api", [requestType.Assembly]);
        await app.StartAsync();

        var endpoint = Assert.Single(
            app.Services.GetRequiredService<EndpointDataSource>().Endpoints.OfType<RouteEndpoint>(),
            candidate => candidate.RoutePattern.RawText!.StartsWith("api/", StringComparison.Ordinal));

        Assert.Equal("POST", GetHttpMethod(endpoint));
        Assert.Equal(GetExpectedPattern(keysCount), endpoint.RoutePattern.RawText);
    }

    [Theory]
    [MemberData(nameof(RouteConstraintMatrix))]
    public async Task UseAutoApi_GeneratesExpectedRouteConstraint(
        Type keyType,
        string expectedKeyPattern)
    {
        var requestType = TypeFactory.CreateSingleKeyApiRequestType(
            $"GetMatrix{keyType.Name}Request",
            keyType);
        await using var app = BuildApp(requestType, taskHandler: false);

        app.UseAutoApi("api", [requestType.Assembly]);
        await app.StartAsync();

        var endpoint = Assert.Single(
            app.Services.GetRequiredService<EndpointDataSource>().Endpoints
                .OfType<RouteEndpoint>(),
            candidate => candidate.RoutePattern.RawText!.StartsWith("api/", StringComparison.Ordinal));

        Assert.Equal($"api/v1/matrix/{expectedKeyPattern}", endpoint.RoutePattern.RawText);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task UseAutoApi_PostCreate_AdvertisesCreatedResponse(bool taskHandler)
    {
        var requestType = TypeFactory.CreateApiRequestType("CreateMatrixRequest", 0);
        await using var app = BuildApp(requestType, taskHandler);

        app.UseAutoApi("api", [requestType.Assembly]);
        await app.StartAsync();

        var endpoint = Assert.Single(
            app.Services.GetRequiredService<EndpointDataSource>().Endpoints
                .OfType<RouteEndpoint>());
        var produces = endpoint.Metadata.GetMetadata<IProducesResponseTypeMetadata>();

        Assert.NotNull(produces);
        Assert.Equal(StatusCodes.Status201Created, produces.StatusCode);
    }

    [Fact]
    public async Task UseAutoApi_WithoutRegisteredHandler_DoesNotGenerateEndpoint()
    {
        var requestType = TypeFactory.CreateApiRequestType("GetMatrixRequest", 0);
        await using var app = BuildAppWithoutHandler();

        app.UseAutoApi("api", [requestType.Assembly]);

        Assert.DoesNotContain(
            app.Services.GetRequiredService<EndpointDataSource>().Endpoints,
            endpoint => endpoint is RouteEndpoint route &&
                        route.RoutePattern.RawText!.StartsWith("api/", StringComparison.Ordinal));
    }

    private static WebApplication BuildApp(Type requestType, bool taskHandler)
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseKestrel();
        builder.Services.AddOnlyMitMediator();
        var responseType = typeof(int);
        var handlerInterface = (taskHandler
                ? typeof(MitMediator.Tasks.IRequestHandler<,>)
                : typeof(IRequestHandler<,>))
            .MakeGenericType(requestType, responseType);
        var handlerType = (taskHandler
                ? typeof(TaskDummyRequestHandler<,>)
                : typeof(DummyRequestHandler<,>))
            .MakeGenericType(requestType, responseType);
        builder.Services.AddTransient(handlerInterface, handlerType);
        var app = builder.Build();
        app.Urls.Add("http://127.0.0.1:0");
        Assert.NotNull(app.Services.GetService(handlerInterface));
        return app;
    }

    private static WebApplication BuildAppWithoutHandler()
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseKestrel();
        builder.Services.AddOnlyMitMediator();
        var app = builder.Build();
        app.Urls.Add("http://127.0.0.1:0");
        return app;
    }

    private static string GetExpectedPattern(int keysCount)
    {
        var pattern = "api/v1/matrix";
        if (keysCount == 0)
        {
            return pattern;
        }

        var keys = keysCount == 1
            ? "{key:int}"
            : string.Join("/", Enumerable.Range(1, keysCount).Select(index => $"{{key{index}:int}}"));
        return $"{pattern}/{keys}";
    }

    private static string GetExpectedHttpMethod(string prefix)
    {
        return prefix switch
        {
            "Get" => "GET",
            "Update" => "PUT",
            "Post" or "Create" => "POST",
            "Delete" => "DELETE",
            _ => throw new ArgumentOutOfRangeException(nameof(prefix), prefix, null)
        };
    }

    private static string GetHttpMethod(RouteEndpoint endpoint)
    {
        return Assert.Single(
            endpoint.Metadata.GetMetadata<HttpMethodMetadata>()!.HttpMethods);
    }

    private static Type GetFileRequestType(int keysCount)
    {
        return keysCount switch
        {
            0 => typeof(ImportMatrix0Request),
            1 => typeof(ImportMatrix1Request),
            2 => typeof(ImportMatrix2Request),
            3 => typeof(ImportMatrix3Request),
            4 => typeof(ImportMatrix4Request),
            5 => typeof(ImportMatrix5Request),
            6 => typeof(ImportMatrix6Request),
            7 => typeof(ImportMatrix7Request),
            _ => throw new ArgumentOutOfRangeException(nameof(keysCount))
        };
    }

    public sealed class DummyRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public ValueTask<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken)
        {
            return ValueTask.FromResult(default(TResponse)!);
        }
    }

    public sealed class TaskDummyRequestHandler<TRequest, TResponse>
        : MitMediator.Tasks.IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(default(TResponse)!);
        }
    }

}

[Tag("matrix")]
[Suffix("")]
public sealed class ImportMatrix0Request : FileRequest, IRequest<int>;

[Tag("matrix")]
[Suffix("")]
public sealed class ImportMatrix1Request : FileRequest, IRequest<int>, IKeyRequest<int>
{
    public int Key { get; init; }
}

[Tag("matrix")]
[Suffix("")]
public sealed class ImportMatrix2Request : FileRequest, IRequest<int>, IKeyRequest<int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
}

[Tag("matrix")]
[Suffix("")]
public sealed class ImportMatrix3Request : FileRequest, IRequest<int>, IKeyRequest<int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
}

[Tag("matrix")]
[Suffix("")]
public sealed class ImportMatrix4Request
    : FileRequest, IRequest<int>, IKeyRequest<int, int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    public int Key4 { get; init; }
}

[Tag("matrix")]
[Suffix("")]
public sealed class ImportMatrix5Request
    : FileRequest, IRequest<int>, IKeyRequest<int, int, int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    public int Key4 { get; init; }
    public int Key5 { get; init; }
}

[Tag("matrix")]
[Suffix("")]
public sealed class ImportMatrix6Request
    : FileRequest, IRequest<int>, IKeyRequest<int, int, int, int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    public int Key4 { get; init; }
    public int Key5 { get; init; }
    public int Key6 { get; init; }
}

[Tag("matrix")]
[Suffix("")]
public sealed class ImportMatrix7Request
    : FileRequest, IRequest<int>, IKeyRequest<int, int, int, int, int, int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public int Key3 { get; init; }
    public int Key4 { get; init; }
    public int Key5 { get; init; }
    public int Key6 { get; init; }
    public int Key7 { get; init; }
}
