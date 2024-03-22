using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//because of conflict
using iResult = SmartSalon.Application.ResultObject.IResult;

namespace SmartSalon.Presentation.Web.Extensions;

public static class ResultExtensions
{
    //TODO: move into ToProblemDetails
    public static object GetErrorsInValidationFormat(this iResult result)
    {
        //match everything between quotes '.....'
        string pattern = @"'([^']*)'";

        return result.Errors!.Select(error =>
        {
            var match = Regex.Match(error.Description, pattern);
            var propertyName = match.Groups[1].Value;
            var errorDescription = error.Description.Remove(match.Index, match.Length);

            return new { Property = propertyName, Error = errorDescription.Trim() };
        });
    }

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
                ["errors"] = result.GetErrorsInValidationFormat()
            },
            Type = "https://www.rfc7807.com/types/bad-request",
        };
    }
}
