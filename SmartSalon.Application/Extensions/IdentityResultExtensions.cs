﻿
using Microsoft.AspNetCore.Identity;

namespace SmartSalon.Application.Extensions;

public static class IdentityResultExtensions
{
    public static string ErrorDescription(this IdentityResult? identityResult) => identityResult!.Errors.First().Description;

    public static bool Failure(this IdentityResult identityResult) => !identityResult!.Succeeded;
}
