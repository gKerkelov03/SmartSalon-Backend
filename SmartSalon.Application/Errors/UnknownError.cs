
namespace SmartSalon.Application.Errors;

public class UnknownError : Error
{
    public UnknownError(string description) : base(description) { }
}
