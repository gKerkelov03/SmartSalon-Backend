
namespace SmartSalon.Application.Errors;

public abstract class Error
{
    public string Description { get; init; }
    public Error(string description) => Description = description;

    public static ConflictError Conflict = new("The operation failed due to conflicting resources");
    public static NotFoundError NotFound = new("Such a resource was not found");
    public static UnauthorizedError Unauthorized = new("You are not authorized to access this resource");
    public static UnknownError Unknown = new("The operation was not successfull");

    public static ValidationError Validation(string propertyName, string description)
        => new ValidationError(propertyName, description);
}
