using System.IO.Pipelines;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using Moq;
using Moq.Protected;

namespace MitMediator.AutoApi.HttpMediator.Tests;

public class GetListQuery : IRequest<GetListResponse>
{
}

public class GetListResponse : ITotalCount
{
    public string[] Items { get; init; }

    public long TotalCount { get; init; }
}

public class GetFileStreamQuery : IRequest<Stream>
{
}

public class GetFileWithNameQuery : IRequest<FileResponse>
{
}

public class GetFileStreamWithNameQuery : IRequest<FileStreamResponse>
{
}

public class GetFileQuery : IRequest<byte[]>
{
}

public class GetCommand : IRequest<string>
{
}

public class PostCommand : IRequest<string>
{
}

public class PutCommand : IRequest<string>
{
}

public class DeleteCommand : IRequest<string>
{
}

public class PostCreateCommand : IRequest<string>
{
}

public class PostSingleKeyCommand : IRequest<string>, IKeyRequest<int>
{
    public int Key { get; init; }
    public string Name { get; init; } = string.Empty;
}

public class PutMultipleKeyCommand : IRequest<string>, IKeyRequest<int, int>
{
    public int Key1 { get; init; }
    public int Key2 { get; init; }
    public string Name { get; init; } = string.Empty;
}

public class HttpRequestHandlerTests
{
    private static HttpRequestHandler<TReq, TResponse> BuildHandler<TReq, TResponse>(MethodType method,
        TResponse? responseContent,
        Func<HttpRequestMessage, Task>? capture = null, bool customPattern = false, bool throwError = false,
        string? errorMessage = null, bool plainText = false)
        where TReq : IRequest<TResponse>
    {
        var msgHandlerMock = new Mock<HttpMessageHandler>();
        msgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Returns(async (HttpRequestMessage request, CancellationToken _) =>
            {
                if (capture is not null)
                {
                    await capture(request);
                }

                if (throwError)
                {
                    return errorMessage == null
                        ? new HttpResponseMessage(HttpStatusCode.BadRequest) { ReasonPhrase = "Bad" }
                        : new HttpResponseMessage(HttpStatusCode.BadRequest)
                            { ReasonPhrase = "Bad", Content = new StringContent(errorMessage) };
                }

                if (typeof(TResponse) == typeof(string))
                {
                    if (plainText)
                    {
                        return new HttpResponseMessage(HttpStatusCode.OK)
                            { Content = new StringContent(responseContent?.ToString() ?? string.Empty, Encoding.UTF8, "text/plain") };
                    }

                    return new HttpResponseMessage(HttpStatusCode.OK)
                        { Content = new StringContent(responseContent?.ToString() ?? string.Empty) };
                }
                
                if (typeof(TResponse) == typeof(Stream))
                {
                    var resp = responseContent as Stream;
                    var context = new StreamContent(resp);
                    var fileResp = new HttpResponseMessage(HttpStatusCode.OK) { Content = context };
                    fileResp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    return fileResp;
                }

                if (typeof(TResponse) == typeof(FileStreamResponse))
                {
                    var resp = responseContent as FileStreamResponse;
                    var context = new StreamContent(resp.File);
                    var fileResp = new HttpResponseMessage(HttpStatusCode.OK) { Content = context };
                    fileResp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    fileResp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = resp.FileName
                    };
                    return fileResp;
                }
                
                if (typeof(TResponse) == typeof(FileResponse))
                {
                    var resp = responseContent as FileResponse;
                    var context = new ByteArrayContent(resp.File);
                    var fileResp = new HttpResponseMessage(HttpStatusCode.OK) { Content = context };
                    fileResp.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    fileResp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = resp.FileName
                    };
                    return fileResp;
                }

                if (responseContent is ITotalCount)
                {
                    var jsonResponse = JsonSerializer.Serialize(responseContent);
                    var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
                        { Content = new StringContent(jsonResponse) };
                    var totalCount = responseContent as ITotalCount;
                    httpResponseMessage.Headers.Add("X-Total-Count", totalCount.TotalCount.ToString());
                    return httpResponseMessage;
                }

                return new HttpResponseMessage(HttpStatusCode.OK)
                    { Content = new ByteArrayContent(responseContent as byte[]) };
            });

        var httpClient = new HttpClient(msgHandlerMock.Object)
        {
            BaseAddress = new Uri("https://host")
        };

        var clientFactory = new Mock<IHttpClientFactory>();
        clientFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var headerMock = new Mock<IHttpHeaderInjector<TReq, TResponse>>();
        headerMock.Setup(h => h.GetHeadersAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(("X-Demo", "value"));

        var services = new ServiceCollection();
        services.AddSingleton(clientFactory.Object);
        services.AddSingleton<IEnumerable<IHttpHeaderInjector<TReq, TResponse>>>(new[] { headerMock.Object });

        return new HttpRequestHandler<TReq, TResponse>(services.BuildServiceProvider(), "https://base", httpClient);
    }

    [Fact]
    public async Task Get_ShouldReturn_Response()
    {
        var handler = BuildHandler<GetCommand, string>(MethodType.Get, "\"get-value\"");
        var result = await handler.HandleAsync(new GetCommand(), CancellationToken.None);
        Assert.Equal("get-value", result);
    }

    [Fact]
    public async Task Get_PlainText_ShouldReturnRawResponse()
    {
        var handler = BuildHandler<GetCommand, string>(
            MethodType.Get,
            "/string%key?value=true",
            plainText: true);

        var result = await handler.HandleAsync(new GetCommand(), CancellationToken.None);

        Assert.Equal("/string%key?value=true", result);
    }

    [Fact]
    public void HttpResponseStream_ShouldUseContentLengthForNonSeekableStream()
    {
        var stream = new NonSeekableReadStream("test"u8.ToArray());
        using var response = new HttpResponseMessage
        {
            Content = new StreamContent(stream)
        };
        response.Content.Headers.ContentLength = 4;
        using var responseStream = new HttpResponseStream(stream, response);

        Assert.False(responseStream.CanSeek);
        Assert.Equal(4, responseStream.Length);
    }

    [Fact]
    public async Task Post_ShouldReturn_Response()
    {
        var handler = BuildHandler<PostCommand, string>(MethodType.Post, "\"post-value\"");
        var result = await handler.HandleAsync(new PostCommand(), CancellationToken.None);
        Assert.Equal("post-value", result);
    }

    [Fact]
    public async Task Put_ShouldReturn_Response()
    {
        var handler = BuildHandler<PutCommand, string>(MethodType.Put, "\"put-value\"");
        var result = await handler.HandleAsync(new PutCommand(), CancellationToken.None);
        Assert.Equal("put-value", result);
    }

    [Fact]
    public async Task Post_ShouldNotSerializeSingleRouteKey()
    {
        string? body = null;
        var handler = BuildHandler<PostSingleKeyCommand, string>(
            MethodType.Post,
            "\"created\"",
            capture: async request => body = await request.Content!.ReadAsStringAsync());

        await handler.HandleAsync(
            new PostSingleKeyCommand { Key = 42, Name = "value" },
            CancellationToken.None);

        var json = JsonDocument.Parse(body!);
        Assert.False(json.RootElement.TryGetProperty("Key", out _));
        Assert.Equal("value", json.RootElement.GetProperty("Name").GetString());
    }

    [Fact]
    public async Task Put_ShouldNotSerializeMultipleRouteKeys()
    {
        string? body = null;
        var handler = BuildHandler<PutMultipleKeyCommand, string>(
            MethodType.Put,
            "\"updated\"",
            capture: async request => body = await request.Content!.ReadAsStringAsync());

        await handler.HandleAsync(
            new PutMultipleKeyCommand { Key1 = 1, Key2 = 2, Name = "value" },
            CancellationToken.None);

        var json = JsonDocument.Parse(body!);
        Assert.False(json.RootElement.TryGetProperty("Key1", out _));
        Assert.False(json.RootElement.TryGetProperty("Key2", out _));
        Assert.Equal("value", json.RootElement.GetProperty("Name").GetString());
    }

    [Fact]
    public async Task Delete_ShouldReturn_Response()
    {
        var handler = BuildHandler<DeleteCommand, string>(MethodType.Delete, "\"delete-value\"");
        var result = await handler.HandleAsync(new DeleteCommand(), CancellationToken.None);
        Assert.Equal("delete-value", result);
    }

    [Fact]
    public async Task PostCreate_ShouldReturn_Response()
    {
        var handler = BuildHandler<PostCreateCommand, string>(MethodType.PostCreate, "\"create-value\"");
        var result = await handler.HandleAsync(new PostCreateCommand(), CancellationToken.None);
        Assert.Equal("create-value", result);
    }

    [Fact]
    public async Task ShouldThrow_HttpRequestException_WhenStatusCodeIsError()
    {
        var handler = BuildHandler<DeleteCommand, string>(
            MethodType.Delete,
            null,
            throwError: true);

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() =>
            handler.HandleAsync(new DeleteCommand(), CancellationToken.None).AsTask());

        Assert.Equal("Bad", exception.Message);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task ShouldThrow_HttpRequestException_WhenStatusCodeIsErrorAndBodyHaveErrorMessage()
    {
        var handler = BuildHandler<DeleteCommand, string>(
            MethodType.Delete,
            null,
            throwError: true,
            errorMessage: "{error:\"error message\"}");

        var exception = await Assert.ThrowsAsync<HttpRequestException>(() =>
            handler.HandleAsync(new DeleteCommand(), CancellationToken.None).AsTask());

        Assert.Equal("{error:\"error message\"}", exception.Message);
        Assert.Equal(HttpStatusCode.BadRequest, exception.StatusCode);
    }

    [Fact]
    public async Task GetFileQuery_ShouldReturn_Response()
    {
        var handler = BuildHandler<GetFileQuery, byte[]>(MethodType.PostCreate, "test"u8.ToArray());
        var result = await handler.HandleAsync(new GetFileQuery(), CancellationToken.None);
        Assert.Equal("test", Encoding.UTF8.GetString(result));
    }
    
    [Fact]
    public async Task GetFileWithNameQuery_ShouldReturn_Response()
    {
        var handler = BuildHandler<GetFileWithNameQuery,
            FileResponse>(MethodType.PostCreate,
            new FileResponse("test"u8.ToArray(), "testfile.txt"));
        var result = await handler.HandleAsync(new GetFileWithNameQuery(), CancellationToken.None);
        Assert.Equal("test", Encoding.UTF8.GetString(result.File));
        Assert.Equal("testfile.txt", result.FileName);
    }

    [Fact]
    public async Task GetFileStreamWithNameQuery_ShouldReturn_Response()
    {
       using var memoryStream = new MemoryStream("test"u8.ToArray());

        var handler = BuildHandler<GetFileStreamWithNameQuery,
            FileStreamResponse>(MethodType.PostCreate,
            new FileStreamResponse(memoryStream, "testfile.txt"));
        var result = await handler.HandleAsync(new GetFileStreamWithNameQuery(), CancellationToken.None);
        memoryStream.Seek(0, SeekOrigin.Begin);
        Assert.Equal("test", Encoding.UTF8.GetString(await result.ReadToEndAsync(CancellationToken.None)));
        Assert.Equal("testfile.txt", result.FileName);
    }
    
    [Fact]
    public async Task GetFileStreamQuery_ShouldReturn_Response()
    {
        using var memoryStream = new MemoryStream("test"u8.ToArray());

        var handler = BuildHandler<GetFileStreamQuery, Stream>(MethodType.PostCreate, memoryStream);
        var result = await handler.HandleAsync(new GetFileStreamQuery(), CancellationToken.None);
        memoryStream.Seek(0, SeekOrigin.Begin);
        using var reader = new StreamReader(result, Encoding.UTF8, leaveOpen: true);
        var content = await reader.ReadToEndAsync();
        Assert.Equal("test", content);
    }

    [Fact]
    public async Task TaskGetListQuery_ShouldReturn_Response()
    {
        var response = new GetListResponse()
        {
            Items = Enumerable.Range(0, 3).Select(x => $"Record #{x}").ToArray(),
            TotalCount = 100
        };
        var handler = BuildHandler<GetListQuery, GetListResponse>(MethodType.Get, response);
        var result = await handler.HandleAsync(new GetListQuery(), CancellationToken.None);
        Assert.Equal(3, result.Items.Length);
        Assert.Equal(100, result.TotalCount);
        Assert.NotStrictEqual(result, response);
    }

    [Fact]
    public async Task JsonResponse_ShouldDisposeResponseAfterConversion()
    {
        var content = new TrackingContent("\"value\"");
        var handler = BuildTrackingHandler<GetCommand, string>(content);

        var result = await handler.HandleAsync(new GetCommand(), CancellationToken.None);

        Assert.Equal("value", result);
        Assert.True(content.IsDisposed);
    }

    [Fact]
    public async Task StreamResponse_ShouldDisposeResponseWhenReturnedStreamIsDisposed()
    {
        var content = new TrackingContent("stream");
        var handler = BuildTrackingHandler<GetFileStreamQuery, Stream>(content);

        var result = await handler.HandleAsync(new GetFileStreamQuery(), CancellationToken.None);

        Assert.False(content.IsDisposed);
        await result.DisposeAsync();
        Assert.True(content.IsDisposed);
    }

    [Fact]
    public async Task InjectedHeaders_ShouldBeScopedToEachRequest()
    {
        var values = new List<string?>();
        var messageHandler = new Mock<HttpMessageHandler>();
        messageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((request, _) =>
                values.Add(request.Headers.GetValues("X-Correlation").Single()))
            .ReturnsAsync(() => new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("\"value\"")
            });
        var httpClient = new HttpClient(messageHandler.Object)
        {
            BaseAddress = new Uri("https://host")
        };
        var injector = new Mock<IHttpHeaderInjector<GetCommand, string>>();
        injector.SetupSequence(i => i.GetHeadersAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(("X-Correlation", "first"))
            .ReturnsAsync(("X-Correlation", "second"));
        var services = new ServiceCollection()
            .AddSingleton(injector.Object)
            .BuildServiceProvider();
        var handler = new HttpRequestHandler<GetCommand, string>(services, null, httpClient);

        await handler.HandleAsync(new GetCommand(), CancellationToken.None);
        await handler.HandleAsync(new GetCommand(), CancellationToken.None);

        Assert.Equal(["first", "second"], values);
        Assert.False(httpClient.DefaultRequestHeaders.Contains("X-Correlation"));
    }

    private static HttpRequestHandler<TRequest, TResponse> BuildTrackingHandler<TRequest, TResponse>(
        HttpContent content)
        where TRequest : IRequest<TResponse>
    {
        var messageHandler = new Mock<HttpMessageHandler>();
        messageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK) { Content = content });
        var httpClient = new HttpClient(messageHandler.Object)
        {
            BaseAddress = new Uri("https://host")
        };
        var services = new ServiceCollection()
            .BuildServiceProvider();

        return new HttpRequestHandler<TRequest, TResponse>(services, "https://base", httpClient);
    }

    private sealed class TrackingContent(string value) : StringContent(value)
    {
        public bool IsDisposed { get; private set; }

        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }

    private sealed class NonSeekableReadStream(byte[] content) : MemoryStream(content)
    {
        public override bool CanSeek => false;

        public override long Length => throw new NotSupportedException();

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }
    }
}