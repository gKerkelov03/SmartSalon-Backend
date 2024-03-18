
using Microsoft.AspNetCore.Identity;

namespace SmartSalon.Shared.Extensions;

public static class IdentityResultExtensions
{
    public static string? GetErrorMessage(this IdentityResult? identityResult)
        => identityResult?.Errors?.First().Description;
}
