
using SmartSalon.Application.Enums;

namespace SmartSalon.Application.ResultObject;

public class ValidationError : Error
{
    public ValidationError(string propertyName, string description) : base(description, ErrorType.Validation)
        => PropertyName = propertyName;

    public string PropertyName { get; init; }
}
