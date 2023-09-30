using System.Linq.Expressions;

namespace Kathanika.Application.Features.Publications.Commands;

internal sealed class UpdatePublicationCommandValidator : AbstractValidator<UpdatePublicationCommand>
{
    public UpdatePublicationCommandValidator(IPublicationRepository publicationRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(publicationRepository.ExistsAsync)
            .WithMessage("Invalid publication");

        RuleFor(x => x.Patch)
            .SetValidator(new PublicationPatchValidator(publicationRepository));
    }
}

internal sealed class PublicationPatchValidator : AbstractValidator<UpdatePublicationCommand.PublicationPatch>
{
    public PublicationPatchValidator(IPublicationRepository publicationRepository)
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.PublicationType)
            .NotNull()
            .IsInEnum();

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
