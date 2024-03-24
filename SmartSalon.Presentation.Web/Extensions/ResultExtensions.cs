using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Errors;
using SmartSalon.Shared.Extensions;

//because of conflict
using iResult = SmartSalon.Application.ResultObject.IResult;

namespace SmartSalon.Presentation.Web.Extensions;

public static class ResultExtensions
{

    public static ProblemDetails ToProblemDetails(this iResult result)
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


        return new ProblemDetails()
        {
            Status = StatusCodes.Status400BadRequest,
            Title = GetTitle(firstError),
            Extensions = new Dictionary<string, object?>()
            {
                ["errors"] = GetErrorsObject(errors)
            },
            Type = GetProblemDetailsType(firstError),
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

        static string GetTitle(Error error) => error switch
        {

            ValidationError => "Bad Request",
            UnauthorizedError => "Unauthorized",
            NotFoundError => "Resource not found",
            ConflictError => "Conflicting resources",
            _ => throw new ArgumentException("Such an Error type is not recognized when trying to construct ProblemDetails response")
        };

        static string GetProblemDetailsType(Error error) => error switch
        {
            ValidationError => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            UnauthorizedError => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
            NotFoundError => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            ConflictError => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            _ => throw new ArgumentException("Such an Error type is not recognized when trying to construct ProblemDetails response")
        };
    }
}