using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Options;

namespace SmartSalon.Application.Services;

public class JwtTokensGenerator(JwtSecurityTokenHandler _jwtHelper, IOptions<JwtOptions> _jwtOptions, TimeProvider _timeProvider)
    : IJwtTokensGenerator
{
    public string GenerateJwt(Id userId, IEnumerable<string> role)
    {
        var jwtOptions = _jwtOptions.Value;
        var signingKeyBytes = Encoding.UTF8.GetBytes(jwtOptions.EncryptionKey);
        var expirationTime = _timeProvider.GetUtcNow().AddDays(jwtOptions.TokenExpirationInDays);

        var roles = role.Append("Something");

        var token = new JwtSecurityToken(
            jwtOptions.Issuer,
            jwtOptions.Audience,
            claims:
            [
                new(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new("roles", string.Join(", ", roles)),
            ],
            expires: expirationTime.DateTime,
            signingCredentials: new(new SymmetricSecurityKey(signingKeyBytes), SecurityAlgorithms.HmacSha256)
        );

        return _jwtHelper.WriteToken(token);
    }
}