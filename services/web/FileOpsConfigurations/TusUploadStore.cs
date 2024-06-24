using Kathanika.Infrastructure.Persistence.FileStorage;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Stores;

namespace Kathanika.Web.FileOpsConfigurations;

internal sealed class TusUploadStore(DefaultTusConfiguration defaultTusConfiguration) : IUploadedStore
{
    private readonly TusDiskStore tusStore = (TusDiskStore)defaultTusConfiguration.Store;
    public async Task DeleteFileAsync(string fileId, CancellationToken cancellationToken = default)
    {
        await tusStore.DeleteFileAsync(fileId, cancellationToken);
    }

    public async Task<bool> FileExistAsync(string fileId, CancellationToken cancellationToken = default)
    {
        return await tusStore.FileExistAsync(fileId, cancellationToken);
    }

    public async Task<Stream> GetFileContentAsync(string fileId, CancellationToken cancellationToken = default)
    {
        ITusFile tusDiskFile = await tusStore.GetFileAsync(fileId, cancellationToken);
        return await tusDiskFile.GetContentAsync(cancellationToken);
    }
}
