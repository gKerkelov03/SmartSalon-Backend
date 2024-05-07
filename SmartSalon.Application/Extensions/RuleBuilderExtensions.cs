using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using System.Linq;
using FluentValidation;
using static SmartSalon.Application.ApplicationConstants.Validation.User;

namespace SmartSalon.Application.Extensions;

public static class RuleBuilderExtensions
{
    private const string _validGuidPattern = "^[{(]?[0-9a-fA-F]{8}-?([0-9a-fA-F]{4}-?){3}[0-9a-fA-F]{12}[)}]?$";

    public static IRuleBuilderOptions<TRequest, Id> MustBeValidGuid<TRequest>(this IRuleBuilder<TRequest, Id> ruleBuilder)
        => ruleBuilder
            .Must((rootObject, propertyValue, context) =>
                Regex.IsMatch(propertyValue.ToString(), _validGuidPattern) && propertyValue != default
            )
            .WithMessage("{PropertyPath}  must be a valid non empty GUID / UUID.");

    public static IRuleBuilderOptions<TRequest, IEnumerable<Id>> MustBeCollectionOfValidGuids<TRequest>(this IRuleBuilder<TRequest, IEnumerable<Id>> ruleBuilder)
        => ruleBuilder
            .Must((rootObject, propertyValue, context) =>
                propertyValue.All(id => Regex.IsMatch(id.ToString(), _validGuidPattern) && propertyValue != default)
            )
            .WithMessage("{PropertyPath}  must be a collection of valid non empty GUID / UUID.");

    public static IRuleBuilderOptions<TRequest, string> MustBeValidPassword<TRequest>(this IRuleBuilder<TRequest, string> ruleBuilder)
        => ruleBuilder
            .Must((rootObject, propertyValue, context) =>
                propertyValue.Length >= MinPasswordLength &&
                propertyValue.Any(char.IsUpper) &&
                propertyValue.Any(char.IsLower) &&
                propertyValue.Any(char.IsDigit)
            )
            .WithMessage("{PropertyPath}  must be at least 6 characters long, with uppercase letter and a digit");
}
