﻿using Asp.Versioning;
using Microsoft.Extensions.Options;

namespace SmartSalon.Presentation.Web.Options.Versioning;

public class ApiVersioningOptionsConfigurator : IConfigureOptions<ApiVersioningOptions>
{
    public void Configure(ApiVersioningOptions options)
    {
        options.DefaultApiVersion = new(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }
}