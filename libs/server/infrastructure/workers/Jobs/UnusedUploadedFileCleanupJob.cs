using Kathanika.Infrastructure.Persistence.FileStorage;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Kathanika.Infrastructure.Workers.Jobs;

[DisallowConcurrentExecution]
internal sealed class UnusedUploadedFileCleanupJob(
    ILogger<UnusedUploadedFileCleanupJob> logger,
    IUploadedStore uploadedStore,
    IFileMetadataService fileMetadataService
) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Started file cleaning Job at {@UnusedUploadedFileCleanupJobStartingTime}", DateTimeOffset.Now);

        await ProcessExpiredFiles(context.CancellationToken);
        await ProcessUnusedFiles(context.CancellationToken);

        logger.LogInformation("Ended file cleaning Job at {@UnusedUploadedFileCleanupJobEndingTime}", DateTimeOffset.Now);
    }

    private async Task ProcessExpiredFiles(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<string> storeExpiredFileIds = await uploadedStore.GetExpiredFileIds(cancellationToken);
        logger.LogInformation("Found {@ExpiredFileCount} expired files in store.", storeExpiredFileIds.Count);
        if (storeExpiredFileIds.Count > 0)
        {
            foreach (string expiredFileId in storeExpiredFileIds)
            {
                logger.LogInformation("Deleting expired file: {@FileIdToDelete}.", expiredFileId);
                await uploadedStore.DeleteFileAsync(expiredFileId, cancellationToken);
                logger.LogInformation("Deleted expired file: {@FileIdToDelete}.", expiredFileId);
            }
            logger.LogInformation("Deleted successfully {@ExpiredFileCount} expired files in store.", storeExpiredFileIds.Count);

            logger.LogInformation("Deleting metadata {@DeletingFileMetadata}", storeExpiredFileIds);
            await fileMetadataService.DeleteAsync([.. storeExpiredFileIds], cancellationToken);
        }
    }

    private async Task ProcessUnusedFiles(CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting unused files");
        IReadOnlyList<string> unusedFileIds = await fileMetadataService.GetUnusedFileIdsAsync(cancellationToken);
        logger.LogInformation("Got {@UnusedFilesCountForCleanup} unused files", unusedFileIds.Count);

        if (unusedFileIds.Count > 0)
        {
            foreach (string unusedFileId in unusedFileIds)
            {
                logger.LogInformation("Deleting unused file {@UnusedFileToDeleteId}", unusedFileId);
                await uploadedStore.DeleteFileAsync(unusedFileId, cancellationToken);
                logger.LogInformation("Deleted unused file {@UnusedFileToDeleteId}", unusedFileId);
            }
            logger.LogInformation("Deleting unused files metadata {UnusedFilesMetadataToDelete}", unusedFileIds);
            await fileMetadataService.DeleteAsync([.. unusedFileIds], cancellationToken);
        }
    }
}
