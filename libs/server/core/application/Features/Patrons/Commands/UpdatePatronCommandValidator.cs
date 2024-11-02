using System.Linq.Expressions;

namespace Kathanika.Core.Application.Features.Patrons.Commands;
internal sealed class UpdatePatronCommandValidator:AbstractValidator<UpdatePatronCommand>
{
    public UpdatePatronCommandValidator(IPatronRepository patronRepository)
    {
        RuleFor(x => x.Id)
            .NotNull()
            .NotEmpty()
            .MustAsync(patronRepository.ExistsAsync)
            .WithMessage("Invalid patron");
        RuleFor(x => new {x.Id, x.Patch})
            .MustAsync(async(props, cancellationToken) =>
            {
                Expression<Func<Patron, bool>> expression = p =>
                    p.Id != props.Id
                    && p.ContactNumber == props.Patch.ContactNumber
                    && p.Email == props.Patch.Email
                    && p.CardNumber == props.Patch.CardNumber;
                bool isDuplicate = await patronRepository.ExistsAsync(expression, cancellationToken);
                return !isDuplicate;
            })
            .WithName("ContactNumber, Email, CardNumber")
            .WithMessage("Duplicate patron information {PropertyName}. Consider updating existing publication's 'Copies Available' field.");
    }
}
