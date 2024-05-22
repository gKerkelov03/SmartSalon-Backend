using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace SmartSalon.Presentation.Web.Extensions;

internal class SecurityStampValidator : ISecurityTokenValidator
{
    public bool CanValidateToken => throw new NotImplementedException();

    public int MaximumTokenSizeInBytes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public bool CanReadToken(string securityToken)
    {
        throw new NotImplementedException();
    }

    public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
    {
        throw new NotImplementedException();
    }
}