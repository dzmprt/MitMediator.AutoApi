namespace MitMediator.AutoApi.Abstractions;

public class FileRequest : IFileRequest
{
    private Stream _file;

    private string _fileName;
    
    public void SetFile(Stream file, string fileName)
    {
        _file = file;
        _fileName = fileName;
    }

    public Stream GetFileStream()
    {
        return _file;
    }
    
    public string GetFileName()
    {
        return _fileName;
    }
    
    public async Task<byte[]> ReadToEndAsync(CancellationToken cancellationToken)
    {
        _file.Seek(0, SeekOrigin.Begin);
        using var memoryStream = new MemoryStream();
        await _file.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}