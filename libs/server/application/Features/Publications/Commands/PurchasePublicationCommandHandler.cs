
namespace Kathanika.Application.Features.Publications.Commands;

internal sealed class PurchasePublicationCommandHandler(
    IPublicationRepository publicationRepository,
    IAuthorRepository authorRepository)
    : IRequestHandler<PurchasePublicationCommand, Publication>
{
    public async Task<Publication> Handle(PurchasePublicationCommand request, CancellationToken cancellationToken)
    {
        Publication? publication;

        if (request.PublicationId is not null)
        {
            publication = await publicationRepository.GetByIdAsync(request.PublicationId, cancellationToken);
            publication!.RecordPurchase(request.UnitPrice, request.Quantity, request.Vendor);
            await publicationRepository.UpdateAsync(publication, cancellationToken);
            return publication;
        }

        IEnumerable<Author> authors = await authorRepository.ListAllAsync(x => request.AuthorIds.Contains(x.Id.ToString()), cancellationToken);
        publication = Publication.Create(
            request.Title!,
            request.Isbn,
            (PublicationType)request.PublicationType!,
            request.Publisher!,
            (DateOnly)request.PublishedDate!,
            request.Edition!,
            request.CallNumber!,
            request.Description!,
            request.Language!,
            request.UnitPrice,
            request.Quantity,
            request.Vendor,
            authors
        );
        Publication addedPublication = await publicationRepository.AddAsync(publication, cancellationToken);
        return addedPublication;
    }
}
