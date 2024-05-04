using System.Linq.Expressions;

namespace Kathanika.Application.Features.Publications.Commands;

internal sealed class PurchasePublicationCommandValidator : AbstractValidator<PurchasePublicationCommand>
{
    public PurchasePublicationCommandValidator(IPublicationRepository publicationRepository)
    {
        RuleFor(x => x.PublicationId)
            .NotNull()
            .MustAsync(async (id, cancellationToken) => await publicationRepository.ExistsAsync(id!, cancellationToken))
            .WithMessage("Must select a valid publication if Title, Publication Type, Call number fields are empty")
            .When(x => x.Title is null || x.PublicationType is null || x.CallNumber is null);

        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .When(x => x.PublicationId is null);

        RuleFor(x => x.CallNumber)
            .NotNull()
            .NotEmpty()
            .When(x => x.PublicationId is null);

        RuleFor(x => x.PublicationType)
            .NotNull()
            .IsInEnum()
            .When(x => x.PublicationId is null);

        RuleFor(x => x.Quantity)
            .NotNull()
            .GreaterThan(0)
            .When(x => x.PublicationId is null);

        RuleFor(x => x.PublishedDate)
            .NotNull()
            .When(x => x.PublicationId is null);

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
            .WithMessage("Duplicate publication information {PropertyName}. Consider purchasing by selecting publication by call number")
            .When(x => x.PublicationId is null);
    }
}
