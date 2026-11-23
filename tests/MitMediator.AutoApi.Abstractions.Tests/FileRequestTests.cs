namespace MitMediator.AutoApi.Abstractions.Tests;

using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class FileRequestTests
{
    [Fact]
    public async Task ReadToEndAsync_ReturnsCorrectBytes()
    {
        // Arrange
        var content = "Hello!";
        var contentBytes = Encoding.UTF8.GetBytes(content);
        using var inputStream = new MemoryStream(contentBytes);

        var fileRequest = new FileRequest();
        fileRequest.SetFile(inputStream, "greeting.txt");

        // Act
        var result = await fileRequest.ReadToEndAsync(CancellationToken.None);

        // Assert
        Assert.Equal(contentBytes, result);
        Assert.Equal("greeting.txt", fileRequest.GetFileName());
        Assert.NotNull(fileRequest.GetFileName());
        Assert.True(fileRequest.GetFileStream().CanRead);
    }

    [Fact]
    public async Task ReadToEndAsync_ResetsStreamPosition()
    {
        // Arrange
        var content = "Stream position test";
        var contentBytes = Encoding.UTF8.GetBytes(content);
        using var inputStream = new MemoryStream(contentBytes);

        var fileRequest = new FileRequest();
        fileRequest.SetFile(inputStream, "position.txt");
        inputStream.Seek(5, SeekOrigin.Begin);

        // Act
        var result = await fileRequest.ReadToEndAsync(CancellationToken.None);

        // Assert
        Assert.Equal(contentBytes, result); // Should read from beginning
    }

    [Fact]
    public async Task ReadToEndAsync_DoesNotSeekNonSeekableStream()
    {
        var content = Encoding.UTF8.GetBytes("streamed content");
        using var inputStream = new NonSeekableStream(content);
        var fileRequest = new FileRequest();
        fileRequest.SetFile(inputStream, "stream.txt");

        var result = await fileRequest.ReadToEndAsync(CancellationToken.None);

        Assert.Equal(content, result);
    }

    [Fact]
    public async Task FileStreamResponse_DoesNotSeekNonSeekableStream()
    {
        var content = Encoding.UTF8.GetBytes("response content");
        using var inputStream = new NonSeekableStream(content);
        var response = new FileStreamResponse(inputStream, "response.txt");

        var result = await response.ReadToEndAsync(CancellationToken.None);

        Assert.Equal(content, result);
    }

    private sealed class NonSeekableStream(byte[] buffer) : MemoryStream(buffer)
    {
        public override bool CanSeek => false;

        public override long Position
        {
            get => throw new NotSupportedException();
            set => throw new NotSupportedException();
        }

        public override long Seek(long offset, SeekOrigin loc) => throw new NotSupportedException();
    }
}