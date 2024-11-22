using Microsoft.Extensions.Options;
using tusdotnet.Interfaces;
using tusdotnet.Stores;

namespace Kathanika.Web.FileOpsConfigurations;

internal class ApplicationTusDiskStore(IOptions<ApplicationOptions> options, ITusFileIdProvider databaseFileIdProvider)
    : TusDiskStore(options.Value.UploadPath,
        true,
        TusDiskBufferSize.Default,
        databaseFileIdProvider);