using System.Text.RegularExpressions;
using FluentValidation;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Extensions;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, Id> MustBeValidGuid<T>(this IRuleBuilder<T, Id> ruleBuilder)
    {
        var pattern = "^[{(]?[0-9a-fA-F]{8}-?([0-9a-fA-F]{4}-?){3}[0-9a-fA-F]{12}[)}]?$";

        return ruleBuilder
            .Must((rootObject, propertyValue, context) =>
                Regex.IsMatch(propertyValue.ToString(), pattern) && propertyValue != default
            )
            .WithMessage("{PropertyPath}  must be a valid non empty GUID / UUID.");
    }

    public static IRuleBuilderOptions<T, string> MustBeValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must((rootObject, propertyValue, context) =>
                propertyValue.Length > MinPasswordLength &&
                propertyValue.Any(char.IsUpper) &&
                propertyValue.Any(char.IsDigit)
            )
            .WithMessage("{PropertyPath}  must be at least 6 characters long, with uppercase and lowercase letters");
    }
}
