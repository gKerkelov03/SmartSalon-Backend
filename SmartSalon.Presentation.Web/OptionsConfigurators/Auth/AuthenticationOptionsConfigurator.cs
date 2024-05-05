using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Options;

namespace SmartSalon.Presentation.Web.Options.Auth;

public class AuthenticationOptionsConfigurator(IConfiguration _config) : IConfigureOptions<JwtBearerOptions>, ITransientLifetime
{
    public void Configure(JwtBearerOptions options)
    {
        var jwtOptions = _config.GetSection(JwtOptions.SectionName).Get<JwtOptions>()!;
        var signingKey = Encoding.ASCII.GetBytes(jwtOptions.EncryptionKey);

        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(signingKey),
        };
    }
}
