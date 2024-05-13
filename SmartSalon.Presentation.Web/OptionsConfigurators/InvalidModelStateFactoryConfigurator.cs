using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;
using SmartSalon.Presentation.Web.Extensions;

namespace SmartSalon.Presentation.Web.OptionsConfigurators;

public class InvalidModelStateFactoryConfigurator : IConfigureOptions<ApiBehaviorOptions>, ITransientLifetime
{
    public void Configure(ApiBehaviorOptions options) => options.InvalidModelStateResponseFactory = context =>
    {
        var identifierForTheJsonObjectAsAWhole = "$";
        var validationErrors = context
            .ModelState
            .Where(kvp => kvp.Value?.Errors.Count > 0)
            .Select(kvp => new
            {
                PropertyName = kvp.Key,
                Errors = kvp.Value!.Errors.Select(error => error.ErrorMessage)
            })
            .SelectMany(validationViolation =>
                validationViolation.Errors.Select(error =>
                    Error.Validation(validationViolation.PropertyName, error)
                )
            )
            .Where(validationError => validationError.PropertyName != "request");

        var result = Result.Failure(validationErrors);
        var requestId = context.HttpContext.TraceIdentifier;

        var isInvalidJson = result.Errors!.Any(error => error.CastTo<ValidationError>().PropertyName == identifierForTheJsonObjectAsAWhole);

        if (isInvalidJson)
        {
            result = Result.Failure(new Error("Invalid JSON"));
        }

        return new ObjectResult(result.ToProblemDetails(requestId));
    };
}
