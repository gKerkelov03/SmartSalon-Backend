namespace SmartSalon.Application.Options;

public class ConnectionStringsOptions
{
    public const string SectionName = "ConnectionStrings";
    public required string Sql { get; set; }
    public required string Redis { get; set; }
}