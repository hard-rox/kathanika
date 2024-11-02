namespace Kathanika.Core.Application.CommonValidators;

internal static class CommonFluentValidators
{
    public static IRuleBuilderOptions<T, string?> ContactNumber<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Matches(@"^\+?\d{1,14}$")
            .WithMessage("Invalid contact number.");
    }
}
