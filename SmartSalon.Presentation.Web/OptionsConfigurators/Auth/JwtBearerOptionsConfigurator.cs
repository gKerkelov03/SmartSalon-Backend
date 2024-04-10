using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Options;

namespace SmartSalon.Presentation.Web.Options.Auth;

public class JwtBearerOptionsConfigurator(IOptions<JwtOptions> _jwtOptions) : IConfigureOptions<JwtBearerOptions>, ISingletonLifetime
{
    public void Configure(JwtBearerOptions options)
    {
        var jwtOptions = _jwtOptions.Value;
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