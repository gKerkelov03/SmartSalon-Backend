namespace SmartSalon.Presentation.Web.Infrastructure.Settings;

public class SettingsProvider : ISettingsProvider
{
    public required string JwtSecret { get; set; }

    public required string ConnectionString { get; set; }
}