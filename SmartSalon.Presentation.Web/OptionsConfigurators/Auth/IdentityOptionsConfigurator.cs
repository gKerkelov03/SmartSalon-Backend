using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Options.Auth;

public class IdentityOptionsConfigurator : IConfigureOptions<IdentityOptions>, ITransientLifetime
{
    public void Configure(IdentityOptions options)
    {
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;

        options.User.RequireUniqueEmail = true;
    }
}
