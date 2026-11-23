namespace MitMediator.AutoApi.HttpMediator;

internal sealed class HttpResponseStream(Stream stream, HttpResponseMessage response) : Stream
{
    private bool _disposed;

    public override bool CanRead => stream.CanRead;
    public override bool CanSeek => stream.CanSeek;
    public override bool CanWrite => stream.CanWrite;
    public override long Length => response.Content.Headers.ContentLength ?? stream.Length;

    public override long Position
    {
        get => stream.Position;
        set => stream.Position = value;
    }

    public override void Flush() => stream.Flush();
    public override int Read(byte[] buffer, int offset, int count) => stream.Read(buffer, offset, count);
    public override long Seek(long offset, SeekOrigin origin) => stream.Seek(offset, origin);
    public override void SetLength(long value) => stream.SetLength(value);
    public override void Write(byte[] buffer, int offset, int count) => stream.Write(buffer, offset, count);

    public override Task<int> ReadAsync(
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken) =>
        stream.ReadAsync(buffer, offset, count, cancellationToken);

    public override ValueTask<int> ReadAsync(
        Memory<byte> buffer,
        CancellationToken cancellationToken = default) =>
        stream.ReadAsync(buffer, cancellationToken);

    protected override void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        if (disposing)
        {
            response.Dispose();
        }

        base.Dispose(disposing);
    }

    public override async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        response.Dispose();
        await base.DisposeAsync();
    }
}
