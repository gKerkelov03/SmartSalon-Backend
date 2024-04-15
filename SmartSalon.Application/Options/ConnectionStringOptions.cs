
namespace SmartSalon.Application.Options;

public class ConnectionStringOptions
{
    public static string SectionName = "ConnectionStrings";
    public required string Sql { get; set; }
    public required string Redis { get; set; }
}