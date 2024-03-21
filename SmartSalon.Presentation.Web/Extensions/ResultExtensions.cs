using System.Text.RegularExpressions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace SmartSalon.Presentation.Web.Extensions;

public static class ResultExtensions
{
    public static object GetErrors(this ResultBase result)
    {
        //match everything between quotes '.....'
        string pattern = @"'([^']*)'";

        return result.Errors.Select(error =>
        {
            Match match = Regex.Match(error.Message, pattern);
            string propertyName = match.Groups[1].Value;
            string errorMessage = error.Message.Remove(match.Index, match.Length);

            return new { Property = propertyName, ErrorMessage = errorMessage.Trim() };
        });
    }

    public static ProblemDetails ToProblemDetails(this ResultBase result)
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
                ["errors"] = result.GetErrors()
            },
            Type = "https://www.rfc7807.com/types/bad-request",
        };
    }
}
