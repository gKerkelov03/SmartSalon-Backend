
namespace SmartSalon.Application.Options;

public class ConnectionStringsOptions
{
    public static string SectionName = "ConnectionStrings";
    public required string Sql { get; set; }
    public required string Redis { get; set; }
}