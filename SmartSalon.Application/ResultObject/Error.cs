using SmartSalon.Application.Enums;

namespace SmartSalon.Application.ResultObject;

public class Error
{
    public string Description { get; init; }
    public ErrorType Type { get; init; }

    public Error(string description, ErrorType type)
    {
        Description = description;
        Type = type;
    }

    public static Error Conflict(string description)
        => new Error(description, ErrorType.Conflict);

    public static Error NotFound(string description)
        => new Error(description, ErrorType.NotFound);

    public static Error Unauthorized(string description)
        => new Error(description, ErrorType.Unauthorized);

    public static ValidationError Validation(string propertyName, string description)
        => new ValidationError(propertyName, description);
}
