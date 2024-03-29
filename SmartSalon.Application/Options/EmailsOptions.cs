namespace SmartSalon.Application.Options;

public class EmailsOptions
{
    public static string SectionName = "Emails";

    public required string Password { get; set; }

    public required string SmtpHost { get; set; }
}