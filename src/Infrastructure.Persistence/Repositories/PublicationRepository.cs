﻿using Kathanika.Domain.Primitives;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PublicationRepository : Repository<Publication>, IPublicationRepository
{
    private const string collectionName = "publications";
    public PublicationRepository(IMongoDatabase database, ILogger<PublicationRepository> logger) : base(database, collectionName, logger)
    {
    }
}
