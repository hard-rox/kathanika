namespace Kathanika.Application.CommonValidators;

internal static class CommonFluentValidators
{
    public static IRuleBuilderOptions<T, string?> ContactNumber<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Matches(@"^\+?\d{1,14}$")
            .WithMessage("Invalid contact number.");
    }

    public static IRuleBuilderOptions<T, string?> Isbn<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Matches(@"^\d{10}(\d{3})?$")
            .WithMessage("Invalid ISBN format.");
    }
}