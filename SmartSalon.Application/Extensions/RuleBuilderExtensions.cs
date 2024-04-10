using System.Text.RegularExpressions;
using FluentValidation;

namespace SmartSalon.Application.Extensions;

public static class RuleBuilderExtensions
{
    public static IRuleBuilderOptions<T, Id> MustBeValidGuid<T>(this IRuleBuilder<T, Id> ruleBuilder)
    {
        var pattern = "^[{(]?[0-9a-fA-F]{8}-?([0-9a-fA-F]{4}-?){3}[0-9a-fA-F]{12}[)}]?$";

        return ruleBuilder
            .Must((rootObject, propertyValue, context) =>
            {
                return Regex.IsMatch(propertyValue.ToString(), pattern) && propertyValue != default;
            })
            .WithMessage("{PropertyPath}  must be a valid non empty GUID / UUID.");
    }
}
