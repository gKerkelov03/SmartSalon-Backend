using FluentResults;

namespace SmartSalon.Presentation.Web.Extensions;

public static class ResultExtensions
{
    public static object ToErrorObject(this ResultBase result)
        => new { Errors = result.Errors.Select(error => error.Message) };
}
