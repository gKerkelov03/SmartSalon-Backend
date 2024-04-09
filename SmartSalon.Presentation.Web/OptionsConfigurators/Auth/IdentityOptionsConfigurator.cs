using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace SmartSalon.Presentation.Web.Options.Auth;

public class IdentityOptionsConfigurator : IConfigureOptions<IdentityOptions>
{
    public void Configure(IdentityOptions options)
    {
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 1;

        options.User.RequireUniqueEmail = true;

        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        options.Lockout.MaxFailedAccessAttempts = 10;
        options.Lockout.AllowedForNewUsers = true;
        // PasswordResetTokenProvider
        // ChangeEmailTokenProvider
        // EmailConfirmationTokenProvider
    }
}
