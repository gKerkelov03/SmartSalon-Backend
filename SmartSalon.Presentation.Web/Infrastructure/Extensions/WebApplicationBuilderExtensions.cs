using static SmartSalon.Presentation.Web.WebConstants;

namespace SmartSalon.Presentation.Web.Infrastructure.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void SetupConfigurationFiles(this WebApplicationBuilder builder)
    {
        builder
            .Configuration
            .SetBasePath(
                Path.Combine(
                    builder.Environment.ContentRootPath,
                    SettingsFilesFolderName
                )
            )
            .AddJsonFile("settings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("settings.development.json", optional: true, reloadOnChange: true);
    }
}