using System.Net.Http.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using MitMediator.AutoApi.Abstractions.Attributes;
using Moq;

namespace MitMediator.AutoApi.Tests.EndpointsMethodsTests;

public class CreatedLocationTests
{
    [Fact]
    public async Task ValueTaskEndpoint_WithSixKeys_ShouldCreateCompleteLocation()
    {
        var request = new CreateWithSixKeys();
        var context = CreateContextForValueTask(new ResourceKeyResponse());
        var endpoint = EndpointsMethods.WithBodyAnd6Keys<
            CreateWithSixKeys, ResourceKeyResponse, int, int, int, int, int, int>(
            new RequestInfo(typeof(CreateWithSixKeys)));

        var result = await (ValueTask<IResult>)endpoint.DynamicInvoke(
            request, 1, 2, 3, 4, 5, 6, context, CancellationToken.None)!;

        var created = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Created<ResourceKeyResponse>>(result);
        Assert.Equal("v1/resources/1/2/3/4/5/6/created", created.Location);
    }

    [Fact]
    public async Task ValueTaskEndpoint_WithSevenKeys_ShouldCreateCompleteLocation()
    {
        var request = new CreateWithSevenKeys();
        var context = CreateContextForValueTask(new ResourceKeyResponse());
        var endpoint = EndpointsMethods.WithBodyAnd7Keys<
            CreateWithSevenKeys, ResourceKeyResponse, int, int, int, int, int, int, int>(
            new RequestInfo(typeof(CreateWithSevenKeys)));

        var result = await (ValueTask<IResult>)endpoint.DynamicInvoke(
            request, 1, 2, 3, 4, 5, 6, 7, context, CancellationToken.None)!;

        var created = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Created<ResourceKeyResponse>>(result);
        Assert.Equal("v1/resources/1/2/3/4/5/6/7/created", created.Location);
    }

    [Fact]
    public async Task TaskEndpoint_WithSixKeys_ShouldCreateCompleteLocation()
    {
        var request = new CreateWithSixKeys();
        var context = CreateContextForTask(new ResourceKeyResponse());
        var endpoint = EndpointsMethodsForTaskHandlers.WithBodyAnd6Keys<
            CreateWithSixKeys, ResourceKeyResponse, int, int, int, int, int, int>(
            new RequestInfo(typeof(CreateWithSixKeys)));

        var result = await (ValueTask<IResult>)endpoint.DynamicInvoke(
            request, 1, 2, 3, 4, 5, 6, context, CancellationToken.None)!;

        var created = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Created<ResourceKeyResponse>>(result);
        Assert.Equal("v1/resources/1/2/3/4/5/6/created", created.Location);
    }

    [Fact]
    public async Task TaskEndpoint_WithSevenKeys_ShouldCreateCompleteLocation()
    {
        var request = new CreateWithSevenKeys();
        var context = CreateContextForTask(new ResourceKeyResponse());
        var endpoint = EndpointsMethodsForTaskHandlers.WithBodyAnd7Keys<
            CreateWithSevenKeys, ResourceKeyResponse, int, int, int, int, int, int, int>(
            new RequestInfo(typeof(CreateWithSevenKeys)));

        var result = await (ValueTask<IResult>)endpoint.DynamicInvoke(
            request, 1, 2, 3, 4, 5, 6, 7, context, CancellationToken.None)!;

        var created = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Created<ResourceKeyResponse>>(result);
        Assert.Equal("v1/resources/1/2/3/4/5/6/7/created", created.Location);
    }

    [Fact]
    public async Task ValueTaskEndpoint_ShouldEncodeRouteAndResourceKeys()
    {
        var request = new CreateWithStringKey();
        var context = CreateContextForValueTask(new ResourceKeyResponse("created/item?draft=true"));
        var endpoint = EndpointsMethods.WithBodyAnd1Key<
            CreateWithStringKey, ResourceKeyResponse, string>(
            new RequestInfo(typeof(CreateWithStringKey)));

        var result = await (ValueTask<IResult>)endpoint.DynamicInvoke(
            request, "folder/item?draft=true", context, CancellationToken.None)!;

        var created = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Created<ResourceKeyResponse>>(result);
        Assert.Equal("v1/resources/folder%2Fitem%3Fdraft%3Dtrue/created%2Fitem%3Fdraft%3Dtrue", created.Location);
    }

    [Fact]
    public async Task TaskEndpoint_ShouldEncodeRouteAndResourceKeys()
    {
        var request = new CreateWithStringKey();
        var context = CreateContextForTask(new ResourceKeyResponse("created/item?draft=true"));
        var endpoint = EndpointsMethodsForTaskHandlers.WithBodyAnd1Key<
            CreateWithStringKey, ResourceKeyResponse, string>(
            new RequestInfo(typeof(CreateWithStringKey)));

        var result = await (ValueTask<IResult>)endpoint.DynamicInvoke(
            request, "folder/item?draft=true", context, CancellationToken.None)!;

        var created = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Created<ResourceKeyResponse>>(result);
        Assert.Equal("v1/resources/folder%2Fitem%3Fdraft%3Dtrue/created%2Fitem%3Fdraft%3Dtrue", created.Location);
    }

    [Fact]
    public async Task ValueTaskEndpoint_OverHttp_ShouldNotDoubleEncodeStringRouteKey()
    {
        var endpoint = EndpointsMethods.WithBodyAnd1Key<
            CreateWithStringKey, ResourceKeyResponse, string>(
            new RequestInfo(typeof(CreateWithStringKey)));

        var location = await GetCreatedLocation(endpoint, CreateContextForValueTask(new ResourceKeyResponse()));

        Assert.Equal(
            "v1/resources/folder%2Fitem%3Fdraft%3Dtrue/created",
            location);
    }

    [Fact]
    public async Task TaskEndpoint_OverHttp_ShouldNotDoubleEncodeStringRouteKey()
    {
        var endpoint = EndpointsMethodsForTaskHandlers.WithBodyAnd1Key<
            CreateWithStringKey, ResourceKeyResponse, string>(
            new RequestInfo(typeof(CreateWithStringKey)));

        var location = await GetCreatedLocation(endpoint, CreateContextForTask(new ResourceKeyResponse()));

        Assert.Equal(
            "v1/resources/folder%2Fitem%3Fdraft%3Dtrue/created",
            location);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task FileEndpoint_WithOneKey_ShouldKeepParentKeyInCreatedLocation(bool taskHandler)
    {
        var request = new CreateFileWithKey();
        var context = taskHandler
            ? CreateFileContextForTask(new ResourceKeyResponse("created"))
            : CreateFileContextForValueTask(new ResourceKeyResponse("created"));
        var endpoint = taskHandler
            ? EndpointsMethodsForTaskHandlers.FormWithFileAnd1Key<
                CreateFileWithKey, ResourceKeyResponse, int>(
                new RequestInfo(typeof(CreateFileWithKey)))
            : EndpointsMethods.FormWithFileAnd1Key<
                CreateFileWithKey, ResourceKeyResponse, int>(
                new RequestInfo(typeof(CreateFileWithKey)));
        using var fileStream = new MemoryStream("test"u8.ToArray());
        var formFile = new FormFile(fileStream, 0, fileStream.Length, "formFile", "test.txt");

        var result = await (ValueTask<IResult>)endpoint.DynamicInvoke(
            formFile, request, 42, context, CancellationToken.None)!;
        var created = Assert.IsType<Microsoft.AspNetCore.Http.HttpResults.Created<ResourceKeyResponse>>(result);

        Assert.Equal("v1/files/42/created", created.Location);
    }

    private static async Task<string?> GetCreatedLocation(Delegate endpoint, DefaultHttpContext dependencyContext)
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseKestrel();
        builder.Services.AddSingleton(dependencyContext.RequestServices.GetRequiredService<IMediator>());
        await using var app = builder.Build();
        app.MapPost("v1/resources/{key}", endpoint);
        app.Urls.Add("http://127.0.0.1:0");
        await app.StartAsync();

        var address = app.Services
            .GetRequiredService<IServer>()
            .Features
            .Get<IServerAddressesFeature>()!
            .Addresses
            .Single();
        using var client = new HttpClient();
        using var response = await client.PostAsync(
            $"{address}/v1/resources/folder%2Fitem%3Fdraft%3Dtrue",
            JsonContent.Create(new { }));

        Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
        return response.Headers.Location?.OriginalString;
    }

    private static DefaultHttpContext CreateContextForValueTask(ResourceKeyResponse response)
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.SendAsync<CreateWithSixKeys, ResourceKeyResponse>(
                It.IsAny<CreateWithSixKeys>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        mediator.Setup(m => m.SendAsync<CreateWithSevenKeys, ResourceKeyResponse>(
                It.IsAny<CreateWithSevenKeys>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        mediator.Setup(m => m.SendAsync<CreateWithStringKey, ResourceKeyResponse>(
                It.IsAny<CreateWithStringKey>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return CreateContext(mediator.Object);
    }

    private static DefaultHttpContext CreateFileContextForValueTask(ResourceKeyResponse response)
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.SendAsync<CreateFileWithKey, ResourceKeyResponse>(
                It.IsAny<CreateFileWithKey>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return CreateContext(mediator.Object);
    }

    private static DefaultHttpContext CreateContextForTask(ResourceKeyResponse response)
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send<CreateWithSixKeys, ResourceKeyResponse>(
                It.IsAny<CreateWithSixKeys>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        mediator.Setup(m => m.Send<CreateWithSevenKeys, ResourceKeyResponse>(
                It.IsAny<CreateWithSevenKeys>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        mediator.Setup(m => m.Send<CreateWithStringKey, ResourceKeyResponse>(
                It.IsAny<CreateWithStringKey>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return CreateContext(mediator.Object);
    }

    private static DefaultHttpContext CreateFileContextForTask(ResourceKeyResponse response)
    {
        var mediator = new Mock<IMediator>();
        mediator.Setup(m => m.Send<CreateFileWithKey, ResourceKeyResponse>(
                It.IsAny<CreateFileWithKey>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);
        return CreateContext(mediator.Object);
    }

    private static DefaultHttpContext CreateContext(IMediator mediator)
    {
        return new DefaultHttpContext
        {
            RequestServices = new ServiceCollection()
                .AddSingleton(mediator)
                .BuildServiceProvider()
        };
    }

    [Method(MethodType.PostCreate)]
    [Pattern("v1/resources/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}")]
    private sealed class CreateWithSixKeys : IRequest<ResourceKeyResponse>,
        IKeyRequest<int, int, int, int, int, int>
    {
        public int Key1 { get; init; }
        public int Key2 { get; init; }
        public int Key3 { get; init; }
        public int Key4 { get; init; }
        public int Key5 { get; init; }
        public int Key6 { get; init; }
    }

    [Method(MethodType.PostCreate)]
    [Pattern("v1/resources/{key1}/{key2}/{key3}/{key4}/{key5}/{key6}/{key7}")]
    private sealed class CreateWithSevenKeys : IRequest<ResourceKeyResponse>,
        IKeyRequest<int, int, int, int, int, int, int>
    {
        public int Key1 { get; init; }
        public int Key2 { get; init; }
        public int Key3 { get; init; }
        public int Key4 { get; init; }
        public int Key5 { get; init; }
        public int Key6 { get; init; }
        public int Key7 { get; init; }
    }

    [Method(MethodType.PostCreate)]
    [Pattern("v1/resources/{key}")]
    private sealed class CreateWithStringKey : IRequest<ResourceKeyResponse>, IKeyRequest<string>
    {
        public string Key { get; init; } = string.Empty;
    }

    [Method(MethodType.PostCreate)]
    [Pattern("v1/files/{key}")]
    private sealed class CreateFileWithKey : FileRequest, IRequest<ResourceKeyResponse>, IKeyRequest<int>
    {
        public int Key { get; init; }
    }

    private sealed class ResourceKeyResponse(string resourceKey = "created") : IResourceKey
    {
        public string GetResourceKey() => resourceKey;
    }
}
