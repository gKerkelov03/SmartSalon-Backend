
namespace SmartSalon.Application.Errors;

public abstract class Error
{
    public string Description { get; init; }

    public Error(string description)
        => Description = description;

    public static ConflictError Conflict(string description)
        => new ConflictError(description);

    public static NotFoundError NotFound(string description)
        => new NotFoundError(description);

    public static UnauthorizedError Unauthorized(string description)
        => new UnauthorizedError(description);

    public static ValidationError Validation(string propertyName, string description)
        => new ValidationError(propertyName, description);
}
