using SmartSalon.Application.Enums;

namespace SmartSalon.Application.ResultObject;

public class Error
{
    public Error(string description, ErrorType type)
    {
        Description = description;
        Type = type;
    }
    public string Description { get; init; }

    public ErrorType Type { get; init; }
}
