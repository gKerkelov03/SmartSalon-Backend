using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsAdminRequirement : IAuthorizationRequirement { }

internal class IsAdminHandler(IHttpContextAccessor _httpContextAccessor) : AuthorizationHandler<IsAdminRequirement>, ISingletonLifetime
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
    {
        var currentUserId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var requestedUserId = _httpContextAccessor.HttpContext.Request.RouteValues["userId"]?.ToString();

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}