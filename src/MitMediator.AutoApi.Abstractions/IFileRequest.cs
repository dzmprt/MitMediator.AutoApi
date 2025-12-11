namespace MitMediator.AutoApi.Abstractions;

/// <summary>
/// File request.
/// </summary>
public interface IFileRequest
{
    /// <summary>
    /// Set file.
    /// </summary>
    /// <param name="file">File stream.</param>
    /// <param name="fileName">File name.</param>
    void SetFile(Stream file, string fileName);
}