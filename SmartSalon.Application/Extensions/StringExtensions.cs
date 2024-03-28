
namespace SmartSalon.Application.Extensions;

public static class StringExtensions
{
    public static Id ToId(this string idAsString) => new Id(idAsString);
}
