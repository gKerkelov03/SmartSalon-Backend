using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;
using SmartSalon.Presentation.Web.Extensions;

namespace SmartSalon.Presentation.Web.Options;

public class ApiBehaviorOptionsConfigurator : IConfigureOptions<ApiBehaviorOptions>, ISingletonLifetime
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
            .SelectMany(validationViolations =>
                validationViolations.Errors.Select(error =>
                    Error.Validation(validationViolations.PropertyName, error)
                )
            );

        var result = Result.Failure(validationErrors);
        var requestId = context.HttpContext.TraceIdentifier;

        if (result.Errors!.Any(error => error.CastTo<ValidationError>().PropertyName == identifierForTheJsonObjectAsAWhole))
        {
            result = Result.Failure(new Error("Invalid JSON"));
        }

        return new ObjectResult(result.ToProblemDetails(requestId));
    };
}
