global using ProfileManager = Microsoft.AspNetCore.Identity.UserManager<SmartSalon.Application.Domain.Profile>;
global using SignInManager = Microsoft.AspNetCore.Identity.SignInManager<SmartSalon.Application.Domain.Profile>;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Models.Requests;
using SmartSalon.Presentation.Web.Options;

namespace SmartSalon.Presentation.Web.Controllers.V1;

//TODO: this is pretty much pseudo code at this point, need to be finished
public class AuthController(
    ProfileManager _profileManager,
    SignInManager _signInManager,
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

        var profileId = Id.NewGuid().ToString();
        var token = GenerateJwtToken(profileId, _jwtOptions.Value, _jwtHelper);

        return Ok(new { Jwt = token });
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
        var credentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256);
        var expirationTime = TimeProvider.System.GetUtcNow().AddDays(jwtOptions.ExpirationInDays).CastTo<DateTime>();

        var token = new JwtSecurityToken(
            _jwtOptions.Value.Issuer,
            _jwtOptions.Value.Audience,
            claims,
            //TODO:start injecting TimeProvider instead of using DateTime 
            expires: expirationTime,
            signingCredentials: credentials
        );

        return jwtHelper.WriteToken(token);
    }
}