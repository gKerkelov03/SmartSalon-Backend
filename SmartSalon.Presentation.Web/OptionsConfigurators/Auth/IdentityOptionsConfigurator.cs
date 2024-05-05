using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Options.Auth;

public class IdentityOptionsConfigurator : IConfigureOptions<IdentityOptions>, ITransientLifetime
{
    public void Configure(IdentityOptions options)
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireUppercase = true;
        options.Password.RequireDigit = true;

        options.User.RequireUniqueEmail = true;

        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
    }
}
