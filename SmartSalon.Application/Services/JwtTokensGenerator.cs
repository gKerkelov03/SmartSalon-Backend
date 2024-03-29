using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Options;

namespace SmartSalon.Application.Services;

public class JwtTokensGenerator(JwtSecurityTokenHandler _jwtHelper, IOptions<JwtOptions> _jwtOptions) : IJwtTokensGenerator
{
    public string GenerateFor(Id userId)
    {
        var jwtOptions = _jwtOptions.Value;
        var signingKeyBytes = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);
        var expirationTime = TimeProvider.System.GetUtcNow().AddDays(jwtOptions.ExpirationInDays).CastTo<DateTime>();

        var token = new JwtSecurityToken(
            jwtOptions.Issuer,
            jwtOptions.Audience,
            claims: [new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())],
            //TODO:start injecting TimeProvider instead of using DateTime 
            expires: expirationTime,
            signingCredentials: new(new SymmetricSecurityKey(signingKeyBytes), SecurityAlgorithms.HmacSha256)
        );

        return _jwtHelper.WriteToken(token);
    }
}