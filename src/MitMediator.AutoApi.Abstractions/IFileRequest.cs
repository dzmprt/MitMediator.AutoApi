namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// File request.
/// </summary>
public interface IFileRequest
{
    /// <summary>
    /// Set file stream.
    /// </summary>
    /// <param name="file">File stream.</param>
    /// <param name="fileName">File name.</param>
    void SetFile(Stream file, string fileName);

    /// <summary>
    /// Get file stream.
    /// </summary>
    /// <returns></returns>
    public Stream GetFileStream();

    /// <summary>
    /// Get file name.
    /// </summary>
    /// <returns></returns>
    public string GetFileName();
}