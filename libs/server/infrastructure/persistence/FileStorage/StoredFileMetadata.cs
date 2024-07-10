namespace Kathanika.Infrastructure.Persistence.FileStorage;

internal sealed class StoredFileMetadata(
    string fileName,
    string contentType,
    long sizeInBytes
)
{
    public string Id { get; private init; } = string.Empty;
    public string FileName { get; private init; } = fileName;
    public string ContentType { get; private init; } = contentType;
    public bool IsUsed { get; private set; } = false;
    public bool IsMoved { get; private set; } = false;
    public long SizeInBytes { get; private set; } = sizeInBytes;
    public string? SubDirectory { get; private set; } = null;
    public DateTimeOffset? UploadCompletedAt { get; private set; } = null;
    public DateTimeOffset? LastMovedAt { get; private set; } = null;
}
