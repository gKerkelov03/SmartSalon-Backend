using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.ResultObject;
using SmartSalon.Shared.Extensions;

//because of conflict
using iResult = SmartSalon.Application.ResultObject.IResult;
using SmartSalon.Application.Enums;

namespace SmartSalon.Presentation.Web.Extensions;

public static class ResultExtensions
{

    public static ProblemDetails ToProblemDetails(this iResult result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException("Cannot make problem details out of successfully executed result");
        }

        return new ProblemDetails()
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Bad Request",
            Extensions = new Dictionary<string, object?>()
            {
                ["errors"] = ConstructErrorsObject(result)
            },
            Type = "https://www.rfc7807.com/types/bad-request",
        };

        static object ConstructErrorsObject(iResult result)
        {
            var errorsObject = new List<object>();

            result.Errors!.ForEach(error =>
            {
                if (error is ValidationError validationError)
                {
                    errorsObject.Add(new
                    {
                        validationError.PropertyName,
                        validationError.Description,
                        validationError.Type
                    });
                }
                else
                {
                    errorsObject.Add(new
                    {
                        error.Description,
                        error.Type
                    });
                }
            });

            return errorsObject;
        }
    }
}