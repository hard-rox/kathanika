using Kathanika.Infrastructure.Persistence.FileStorage;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Stores;

namespace Kathanika.Web.FileOpsConfigurations;

internal sealed class TusUploadStore(DefaultTusConfiguration defaultTusConfiguration) : IUploadedStore
{
    private readonly TusDiskStore _tusStore = (TusDiskStore)defaultTusConfiguration.Store;
    public async Task DeleteFileAsync(string fileId, CancellationToken cancellationToken = default)
    {
        await _tusStore.DeleteFileAsync(fileId, cancellationToken);
    }

    public async Task<bool> FileExistAsync(string fileId, CancellationToken cancellationToken = default)
    {
        return await _tusStore.FileExistAsync(fileId, cancellationToken);
    }

    public async Task<List<string>> GetExpiredFileIds(CancellationToken cancellationToken = default)
    {
        IEnumerable<string> expiredFileIds = await _tusStore.GetExpiredFilesAsync(cancellationToken);
        return expiredFileIds.ToList();
    }

    public async Task<Stream> GetFileContentAsync(string fileId, CancellationToken cancellationToken = default)
    {
        ITusFile tusDiskFile = await _tusStore.GetFileAsync(fileId, cancellationToken);
        return await tusDiskFile.GetContentAsync(cancellationToken);
    }
}
