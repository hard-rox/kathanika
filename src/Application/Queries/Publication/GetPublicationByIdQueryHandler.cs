using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Queries;

internal sealed class GetPublicationByIdQueryHandler
    : IRequestHandler<GetPublicationByIdQuery, Publication>
{
    private readonly IPublicationRepository publicationRepository;

    public GetPublicationByIdQueryHandler(IPublicationRepository publicationRepository)
    {
        this.publicationRepository = publicationRepository;
    }

    public async Task<Publication> Handle(GetPublicationByIdQuery request, CancellationToken cancellationToken)
    {
        var publication = await publicationRepository.GetByIdAsync(request.Id);
        return publication;
    }
}
