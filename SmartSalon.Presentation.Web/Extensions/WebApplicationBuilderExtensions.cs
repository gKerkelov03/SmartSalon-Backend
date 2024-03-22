using Serilog;
using static SmartSalon.Presentation.Web.WebConstants;

namespace SmartSalon.Presentation.Web.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureSerilogFromTheConfigurationFiles(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, loggerConfig)
            => loggerConfig.ReadFrom.Configuration(context.Configuration));

        return builder;
    }

    public static WebApplicationBuilder SetupConfigurationFiles(this WebApplicationBuilder builder)
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

        return builder;
    }
}