using SmartSalon.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Application.Services;

public class CurrentUserAccessor(IHttpContextAccessor _httpContextAccessor) : ICurrentUserAccessor
{
    public Id? Id
        => _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.CastTo<Id>();
}