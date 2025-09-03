using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using MitMediator.AutoApi.Abstractions;
using Moq;

namespace MitMediator.AutoApi.Tests.EndpointsMethodsTests;

public class FormWithFileTests
{
    [Fact]
    public async Task FormWithFile_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        RequestsForTests.Files.Commands.ImportFile.ImportFileCommand capturedCommand = null;

        mediatorMock
            .Setup(m => m.SendAsync<RequestsForTests.Files.Commands.ImportFile.ImportFileCommand, FileStreamResponse>(
                It.IsAny<RequestsForTests.Files.Commands.ImportFile.ImportFileCommand>(),
                It.IsAny<CancellationToken>()))
            .Callback<RequestsForTests.Files.Commands.ImportFile.ImportFileCommand, CancellationToken>((cmd, _) =>
                capturedCommand = cmd)
            .ReturnsAsync(() => new FileStreamResponse(capturedCommand.File, capturedCommand.FileName));

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var formFile = CreateTestFormFile("test", "test.txt");
        var request = new RequestsForTests.Files.Commands.ImportFile.ImportFileCommand();

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .FormWithFile<RequestsForTests.Files.Commands.ImportFile.ImportFileCommand, FileStreamResponse>(new RequestInfo(typeof(RequestsForTests.Files.Commands.ImportFile.ImportFileCommand)));
        var result = await (ValueTask<IResult>)del.DynamicInvoke(formFile, request, context, CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileStreamHttpResult>(result);
        Assert.Equal("test.txt", GetFileName(context.Response));
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }

    [Fact]
    public async Task FormWithFileAnd1Key_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        RequestsForTests.Files.Commands.ImportFileWithKeyCommand.ImportFileWithKeyCommand capturedCommand = null;

        mediatorMock
            .Setup(m => m
                .SendAsync<RequestsForTests.Files.Commands.ImportFileWithKeyCommand.ImportFileWithKeyCommand,
                    FileStreamResponse>(
                    It.IsAny<RequestsForTests.Files.Commands.ImportFileWithKeyCommand.ImportFileWithKeyCommand>(),
                    It.IsAny<CancellationToken>()))
            .Callback<RequestsForTests.Files.Commands.ImportFileWithKeyCommand.ImportFileWithKeyCommand,
                CancellationToken>((cmd, _) => capturedCommand = cmd)
            .ReturnsAsync(() => new FileStreamResponse(capturedCommand.File, capturedCommand.FileName));

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var formFile = CreateTestFormFile("test", "test.txt");
        var request = new RequestsForTests.Files.Commands.ImportFileWithKeyCommand.ImportFileWithKeyCommand();
        var key = 1;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .FormWithFileAnd1Key<RequestsForTests.Files.Commands.ImportFileWithKeyCommand.ImportFileWithKeyCommand,
                FileStreamResponse, int>(new RequestInfo(typeof(RequestsForTests.Files.Commands.ImportFileWithKeyCommand.ImportFileWithKeyCommand)));
        var result =
            await (ValueTask<IResult>)del.DynamicInvoke(formFile, request, key, context, CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileStreamHttpResult>(result);
        Assert.Equal("test.txt", GetFileName(context.Response));
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }

    [Fact]
    public async Task FormWithFileAnd2Key_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        RequestsForTests.Files.Commands.ImportFileWithKey2Command.ImportFileWithKey2Command capturedCommand = null;

        mediatorMock
            .Setup(m => m
                .SendAsync<RequestsForTests.Files.Commands.ImportFileWithKey2Command.ImportFileWithKey2Command,
                    FileStreamResponse>(
                    It.IsAny<RequestsForTests.Files.Commands.ImportFileWithKey2Command.ImportFileWithKey2Command>(),
                    It.IsAny<CancellationToken>()))
            .Callback<RequestsForTests.Files.Commands.ImportFileWithKey2Command.ImportFileWithKey2Command,
                CancellationToken>((cmd, _) => capturedCommand = cmd)
            .ReturnsAsync(() => new FileStreamResponse(capturedCommand.File, capturedCommand.FileName));

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var formFile = CreateTestFormFile("test", "test.txt");
        var request = new RequestsForTests.Files.Commands.ImportFileWithKey2Command.ImportFileWithKey2Command();
        var key1 = 1;
        var key2 = 2;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .FormWithFileAnd2Key<RequestsForTests.Files.Commands.ImportFileWithKey2Command.ImportFileWithKey2Command,
                FileStreamResponse, int, int>(new RequestInfo(typeof(RequestsForTests.Files.Commands.ImportFileWithKey2Command.ImportFileWithKey2Command)));
        var result =
            await (ValueTask<IResult>)del.DynamicInvoke(formFile, request, key1, key2, context,
                CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileStreamHttpResult>(result);
        Assert.Equal("test.txt", GetFileName(context.Response));
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }

    [Fact]
    public async Task FormWithFileAnd3Key_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        RequestsForTests.Files.Commands.ImportFileWithKey3Command.ImportFileWithKey3Command capturedCommand = null;

        mediatorMock
            .Setup(m => m
                .SendAsync<RequestsForTests.Files.Commands.ImportFileWithKey3Command.ImportFileWithKey3Command,
                    FileStreamResponse>(
                    It.IsAny<RequestsForTests.Files.Commands.ImportFileWithKey3Command.ImportFileWithKey3Command>(),
                    It.IsAny<CancellationToken>()))
            .Callback<RequestsForTests.Files.Commands.ImportFileWithKey3Command.ImportFileWithKey3Command,
                CancellationToken>((cmd, _) => capturedCommand = cmd)
            .ReturnsAsync(() => new FileStreamResponse(capturedCommand.File, capturedCommand.FileName));

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var formFile = CreateTestFormFile("test", "test.txt");
        var request = new RequestsForTests.Files.Commands.ImportFileWithKey3Command.ImportFileWithKey3Command();
        var key1 = 1;
        var key2 = 2;
        var key3 = 3;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .FormWithFileAnd3Key<RequestsForTests.Files.Commands.ImportFileWithKey3Command.ImportFileWithKey3Command,
                FileStreamResponse, int, int, int>(new RequestInfo(typeof(RequestsForTests.Files.Commands.ImportFileWithKey3Command.ImportFileWithKey3Command)));
        var result =
            await (ValueTask<IResult>)del.DynamicInvoke(formFile, request, key1, key2, key3, context,
                CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileStreamHttpResult>(result);
        Assert.Equal("test.txt", GetFileName(context.Response));
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }

    [Fact]
    public async Task FormWithFileAnd4Key_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        RequestsForTests.Files.Commands.ImportFileWithKey4Command.ImportFileWithKey4Command capturedCommand = null;

        mediatorMock
            .Setup(m => m
                .SendAsync<RequestsForTests.Files.Commands.ImportFileWithKey4Command.ImportFileWithKey4Command,
                    FileStreamResponse>(
                    It.IsAny<RequestsForTests.Files.Commands.ImportFileWithKey4Command.ImportFileWithKey4Command>(),
                    It.IsAny<CancellationToken>()))
            .Callback<RequestsForTests.Files.Commands.ImportFileWithKey4Command.ImportFileWithKey4Command,
                CancellationToken>((cmd, _) => capturedCommand = cmd)
            .ReturnsAsync(() => new FileStreamResponse(capturedCommand.File, capturedCommand.FileName));

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var formFile = CreateTestFormFile("test", "test.txt");
        var request = new RequestsForTests.Files.Commands.ImportFileWithKey4Command.ImportFileWithKey4Command();
        var key1 = 1;
        var key2 = 2;
        var key3 = 3;
        var key4 = 4;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .FormWithFileAnd4Key<RequestsForTests.Files.Commands.ImportFileWithKey4Command.ImportFileWithKey4Command,
                FileStreamResponse, int, int, int, int>(new RequestInfo(typeof(RequestsForTests.Files.Commands.ImportFileWithKey4Command.ImportFileWithKey4Command)));
        var result =
            await (ValueTask<IResult>)del.DynamicInvoke(formFile, request, key1, key2, key3, key4, context,
                CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileStreamHttpResult>(result);
        Assert.Equal("test.txt", GetFileName(context.Response));
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }

    [Fact]
    public async Task FormWithFileAnd5Key_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        RequestsForTests.Files.Commands.ImportFileWithKey5Command.ImportFileWithKey5Command capturedCommand = null;

        mediatorMock
            .Setup(m => m
                .SendAsync<RequestsForTests.Files.Commands.ImportFileWithKey5Command.ImportFileWithKey5Command,
                    FileStreamResponse>(
                    It.IsAny<RequestsForTests.Files.Commands.ImportFileWithKey5Command.ImportFileWithKey5Command>(),
                    It.IsAny<CancellationToken>()))
            .Callback<RequestsForTests.Files.Commands.ImportFileWithKey5Command.ImportFileWithKey5Command,
                CancellationToken>((cmd, _) => capturedCommand = cmd)
            .ReturnsAsync(() => new FileStreamResponse(capturedCommand.File, capturedCommand.FileName));

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var formFile = CreateTestFormFile("test", "test.txt");
        var request = new RequestsForTests.Files.Commands.ImportFileWithKey5Command.ImportFileWithKey5Command();
        var key1 = 1;
        var key2 = 2;
        var key3 = 3;
        var key4 = 4;
        var key5 = 5;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .FormWithFileAnd5Key<RequestsForTests.Files.Commands.ImportFileWithKey5Command.ImportFileWithKey5Command,
                FileStreamResponse, int, int, int, int, int>(new RequestInfo(typeof(RequestsForTests.Files.Commands.ImportFileWithKey5Command.ImportFileWithKey5Command)));
        var result =
            await (ValueTask<IResult>)del.DynamicInvoke(formFile, request, key1, key2, key3, key4, key5, context,
                CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileStreamHttpResult>(result);
        Assert.Equal("test.txt", GetFileName(context.Response));
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }

    [Fact]
    public async Task FormWithFileAnd6Key_ShouldReturnFileCorrectly()
    {
        // Arrange
        var mediatorMock = new Mock<IMediator>();
        RequestsForTests.Files.Commands.ImportFileWithKey6Command.ImportFileWithKey6Command capturedCommand = null;

        mediatorMock
            .Setup(m => m
                .SendAsync<RequestsForTests.Files.Commands.ImportFileWithKey6Command.ImportFileWithKey6Command,
                    FileStreamResponse>(
                    It.IsAny<RequestsForTests.Files.Commands.ImportFileWithKey6Command.ImportFileWithKey6Command>(),
                    It.IsAny<CancellationToken>()))
            .Callback<RequestsForTests.Files.Commands.ImportFileWithKey6Command.ImportFileWithKey6Command,
                CancellationToken>((cmd, _) => capturedCommand = cmd)
            .ReturnsAsync(() => new FileStreamResponse(capturedCommand.File, capturedCommand.FileName));

        var services = new ServiceCollection()
            .AddSingleton(mediatorMock.Object)
            .AddLogging()
            .BuildServiceProvider();

        var context = new DefaultHttpContext { RequestServices = services };
        var formFile = CreateTestFormFile("test", "test.txt");
        var request = new RequestsForTests.Files.Commands.ImportFileWithKey6Command.ImportFileWithKey6Command();
        var key1 = 1;
        var key2 = 2;
        var key3 = 3;
        var key4 = 4;
        var key5 = 5;
        var key6 = 6;

        using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;

        var del = EndpointsMethods
            .FormWithFileAnd6Key<RequestsForTests.Files.Commands.ImportFileWithKey6Command.ImportFileWithKey6Command,
                FileStreamResponse, int, int, int, int, int, int>(new RequestInfo(typeof(RequestsForTests.Files.Commands.ImportFileWithKey6Command.ImportFileWithKey6Command)));
        var result =
            await (ValueTask<IResult>)del.DynamicInvoke(formFile, request, key1, key2, key3, key4, key5, key6, context,
                CancellationToken.None)!;

        // Act
        await result.ExecuteAsync(context);

        // Assert
        Assert.IsType<FileStreamHttpResult>(result);
        Assert.Equal("test.txt", GetFileName(context.Response));
        Assert.Equal(4, context.Response.ContentLength);

        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseText = await new StreamReader(memoryStream).ReadToEndAsync();
        Assert.Equal("test", responseText);
    }

    private static IFormFile CreateTestFormFile(string content, string fileName)
    {
        var bytes = Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(bytes);

        return new FormFile(stream, 0, bytes.Length, "file", fileName)
        {
            Headers = new HeaderDictionary()
        };
    }
    

    private static string? GetFileName(HttpResponse response)
    {
        if (response.Headers.TryGetValue("Content-Disposition", out var contentDisposition))
        {
            var disposition = contentDisposition.ToString();
            var filenamePart = disposition.Split(';')
                .FirstOrDefault(p => p.Trim().StartsWith("filename=", StringComparison.OrdinalIgnoreCase));

            if (filenamePart != null)
            {
                var filename = filenamePart.Split('=')[1].Trim().Trim('"');
                return filename;
            }
        }

        return null;
    }
}