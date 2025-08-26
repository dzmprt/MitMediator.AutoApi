namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// File response with custom file name.
/// </summary>
public sealed class FileResponse(byte[] data, string fileName)
{
    public string FileName { get; } = fileName;
    public byte[] Data { get; } = data;
}