using SmartSalon.Web.Infrastructure.Settings;
using static SmartSalon.Web.WebConstants;

namespace SmartSalon.Web.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static string? GetDefaultConnectionString(this IConfiguration config) =>
        config?.GetConnectionString("DefaultConnection");

    public static ISettingsProvider GetSettingsProvider(this IConfiguration config)
    {
        var settingsProviderConfig = config.GetSection(SettingsSectionName);
        var nullableSettingsProvider = settingsProviderConfig.Get<SettingsProvider>();

        var defaultSettingsProvider = new SettingsProvider
        {
            JwtSecret = "goshoisunicorn",
            ConnectionString = "Server=.;Database=;Trusted_Connection=True;TrustServerCertificate=True;"
        };

        if (nullableSettingsProvider is null)
        {
            return defaultSettingsProvider;
        }

        var settingsProvider = new SettingsProvider()
        {
            JwtSecret = nullableSettingsProvider.JwtSecret
                ?? defaultSettingsProvider.JwtSecret,

            ConnectionString = nullableSettingsProvider.ConnectionString
                ?? defaultSettingsProvider.JwtSecret
        };

        return settingsProvider;
    }
}
