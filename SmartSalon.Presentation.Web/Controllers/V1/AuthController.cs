using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Models.Requests;
using SmartSalon.Presentation.Web.Options;

namespace SmartSalon.Presentation.Web.Controllers.V1;

public class AuthController(
    UserManager<Profile> _profileManager,
    SignInManager<Profile> _signInManager,
    IOptions<JwtOptions> _jwtOptions,
    JwtSecurityTokenHandler _jwtHelper
) : ApiController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _profileManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return BadRequest("Invalid username or password.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return BadRequest("Invalid username or password.");
        }

        var token = GenerateJwtToken(Id.NewGuid().ToString(), _jwtOptions.Value, _jwtHelper);

        return Ok(new { Token = token });
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest requests)
    {
        // var user = new Profile
        // var result = await _profileManager.CreateAsync(user, request.Password);
        if (false)//!result.Succeeded)
        {
            return BadRequest("Failed to register user.");
        }

        var token = GenerateJwtToken(Id.NewGuid().ToString(), _jwtOptions.Value, _jwtHelper);

        return Ok(new { Token = token });
    }

    private string GenerateJwtToken(string userId, JwtOptions jwtOptions, JwtSecurityTokenHandler jwtHelper)
    {
        var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, userId), };
        var signingKey = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);
        var creds = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256);

        // DateTime.UtcNow.AddDays(jwtOptions.ExpirationInDays),

        var token = new JwtSecurityToken(
            _jwtOptions.Value.Issuer,
            _jwtOptions.Value.Audience,
            claims,
            //TODO: accept the datetime from the constructor
            expires: TimeProvider.System.GetUtcNow().AddDays(jwtOptions.ExpirationInDays).CastTo<DateTime>(),
            signingCredentials: creds
        );

        return jwtHelper.WriteToken(token);
    }
}