using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;
using SmartSalon.Shared.Extensions;

namespace SmartSalon.Presentation.Web.Extensions;

public static class ResultExtensions
{
    public static ProblemDetails ToProblemDetails(this IResult result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot make problem details out of successfully executed result");
        }

        IEnumerable<Error> errors;
        var validationErrors = result.Errors!.Where(error => error is ValidationError);
        var firstError = result.Errors!.First();

        if (validationErrors.Any())
        {
            errors = validationErrors;
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
                ["errors"] = GetErrorsObject(errors)
            },
            Type = type
        };

        static object GetErrorsObject(IEnumerable<Error> errors)
        {
            var errorsObject = new List<object>();

            errors.ForEach(error =>
            {
                if (error is ValidationError validationError)
                {
                    errorsObject.Add(new
                    {
                        validationError.PropertyName,
                        validationError.Description,
                    });
                }
                else
                {
                    errorsObject.Add(error.Description);
                }
            });

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