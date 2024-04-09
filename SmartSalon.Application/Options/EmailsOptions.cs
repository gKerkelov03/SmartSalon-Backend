namespace SmartSalon.Application.Options;

public class EmailsOptions
{
    public static string SectionName = "Emails";
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Host { get; set; }
    public required int Port { get; set; }
}