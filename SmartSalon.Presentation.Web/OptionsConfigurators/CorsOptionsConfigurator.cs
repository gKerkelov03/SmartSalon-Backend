﻿using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Options;

namespace SmartSalon.Presentation.Web.Options;

public class CorsOptionsConfigurator : IConfigureOptions<CorsOptions>
{
    public void Configure(CorsOptions options)
        => options.AddPolicy
        (
            "Angular-Localhost-Cors-Policy",
            policy => policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
        );
}
