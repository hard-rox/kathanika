using System.Linq.Expressions;

namespace Kathanika.Application.Features.Publications.Commands;

internal sealed class AddPublicationCommandValidator : AbstractValidator<AcquirePublicationCommand>
{
    public AddPublicationCommandValidator(
        IPublicationRepository publicationRepository,
        IPublisherRepository publisherRepository)
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.PublicationType)
            .NotNull()
            .IsInEnum();
        RuleFor(x => x.Quantity)
            .NotNull()
            .GreaterThan(0);
        RuleFor(x => x.AcquisitionMethod)
            .NotNull()
            .IsInEnum();

        RuleFor(x => new { x.UnitPrice, x.Vendor })
            .NotNull()
            .When(x => x.AcquisitionMethod == AcquisitionMethod.Purchase)
            .WithName("Unit Price, Vendor")
            .WithMessage("{PropertyName} is mandatory when purchase.");

        RuleFor(x => new { x.Patron })
            .NotNull()
            .When(x => x.AcquisitionMethod == AcquisitionMethod.Donation)
            .WithName("Patron")
            .WithMessage("{PropertyName} is mandatory when take donation.");

        RuleFor(x => new { x.PublisherId })
            .MustAsync(async (prop, cancellationToken) =>
            {
                bool hasPublisher = await publisherRepository.ExistsAsync(prop.PublisherId, cancellationToken);
                return hasPublisher;
            })
            .When(x => x.PublisherId is not null);

        RuleFor(x => new { x.Title, x.Isbn, x.PublicationType, x.Edition })
            .MustAsync(async (props, cancellationToken) =>
            {
                Expression<Func<Publication, bool>> expression = p =>
                    p.Title == props.Title
                    && p.Isbn == props.Isbn
                    && p.PublicationType == props.PublicationType
                    && p.Edition == props.Edition;
                bool isDuplicate = await publicationRepository.ExistsAsync(expression, cancellationToken);
                return !isDuplicate;
            })
            .WithName("Title, ISBN, PublicationType, Edition")
            .WithMessage("Duplicate publication information {PropertyName}. Consider updating existing publication's 'Copies Available' field.");
    }
}
