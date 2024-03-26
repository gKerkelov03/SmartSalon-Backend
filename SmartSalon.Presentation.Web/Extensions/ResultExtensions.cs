using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Presentation.Web.Extensions;

public static class ResultExtensions
{
    public static ProblemDetails ToProblemDetails(this IResult result, string traceId)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot make problem details out of successfully executed result");
        }

        IEnumerable<Error> errors;
        var validationErrors = result.Errors!.OfType<ValidationError>();
        var firstError = result.Errors!.First();

        if (validationErrors.Any())
        {
            errors = validationErrors;
            firstError = validationErrors.First();
        }
        else
        {
            errors = [firstError];
        }

        var (title, statusCode, type) = GetProblemDetailsInfoFor(firstError);

        return new ProblemDetails()
        {
            Status = statusCode,
            Title = title,
            Extensions = new Dictionary<string, object?>()
            {
                ["errors"] = GetErrorsObject(errors),
                [nameof(traceId)] = traceId
            },
            Type = type
        };

        static object GetErrorsObject(IEnumerable<Error> errors)
        {
            var errorsObject = new List<object>();
            var validationErrorGroups = errors.OfType<ValidationError>().GroupBy(error => error.PropertyName);

            validationErrorGroups.ForEach(group =>
            {
                var propertyName = group.Key;
                var validationViolations = group.Select(validationError => validationError.Description);

                errorsObject.Add(new { propertyName, validationViolations });
            });

            // errorsObject.Add(validationError.Description);

            return errorsObject;
        }

        static (string title, int statusCode, string type) GetProblemDetailsInfoFor(Error error)
            => error switch
            {

                ValidationError => ("Bad Request", Status400BadRequest, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"),
                UnauthorizedError => ("Unauthorized", Status401Unauthorized, "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1"),
                NotFoundError => ("Resource not found", Status404NotFound, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4"),
                ConflictError => ("Conflicting resources", Status409Conflict, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8"),
                _ => throw new ArgumentException("Such an Error type is not recognized when trying to construct ProblemDetails response")
            };
    }
}