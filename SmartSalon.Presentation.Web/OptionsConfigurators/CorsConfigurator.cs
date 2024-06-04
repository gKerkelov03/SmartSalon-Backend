using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.OptionsConfigurators;

public class CorsOptionsConfigurator : IConfigureOptions<CorsOptions>, ITransientLifetime
{
    public void Configure(CorsOptions options)
        => options.AddPolicy
        (
            ReactAndAngularLocalhostCorsPolicy,
            policy => policy
                .WithOrigins("http://localhost:4200")
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
        );
}
