using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using iResult = SmartSalon.Application.ResultObject.IResult;

namespace SmartSalon.Presentation.Web.Extensions;

internal static class ResultExtensions
{

    public static ProblemDetails ToProblemDetails(this iResult result, string requestId)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot make problem details out of successfully executed result");
        }

        Error errorToGetProblemDetailsInfoFrom;
        object errorsResponseObject;
        var validationErrors = result.Errors!.OfType<ValidationError>();

        if (validationErrors.Any())
        {
            errorToGetProblemDetailsInfoFrom = validationErrors.First();
            errorsResponseObject = GetValidationErrorsObject(validationErrors);
        }
        else
        {
            errorToGetProblemDetailsInfoFrom = result.Errors!.First();
            errorsResponseObject = result.Errors!.Select(error => error.Description);
        }

        var (title, statusCode, type) = GetProblemDetailsInfoFor(errorToGetProblemDetailsInfoFrom);

        return new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Extensions = new Dictionary<string, object?>()
            {
                ["errors"] = errorsResponseObject,
                [nameof(requestId)] = requestId
            },
            Type = type
        };

        static (string title, int statusCode, string type) GetProblemDetailsInfoFor(Error error) => error switch
        {
            ValidationError => ("Bad Request", Status400BadRequest, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"),
            UnauthorizedError => ("Unauthorized", Status401Unauthorized, "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1"),
            NotFoundError => ("Resource not found", Status404NotFound, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4"),
            ConflictError => ("Conflicting resources", Status409Conflict, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8"),
            Error => ("Bad Request", Status400BadRequest, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1"),
            _ => throw new ArgumentException("Such an Error type is not recognized when trying to construct ProblemDetails response")
        };

        static object GetValidationErrorsObject(IEnumerable<ValidationError> errors)
        {
            var errorsObject = new List<object>();
            var validationErrorGroups = errors.OfType<ValidationError>().GroupBy(error => error.PropertyName);

            validationErrorGroups.ForEach(group =>
            {
                var propertyName = group.Key;
                var validationViolations = group.Select(validationError => validationError.Description);

                errorsObject.Add(new { propertyName, validationViolations });
            });

            return errorsObject;
        }
    }
}