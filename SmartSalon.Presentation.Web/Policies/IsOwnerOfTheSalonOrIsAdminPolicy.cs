using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsOwnerOfTheSalonOrIsAdminRequirement : IAuthorizationRequirement { }

internal class IsOwnerOfTheSalonOrAdminHandler(IHttpContextAccessor _httpContextAccessor) : AuthorizationHandler<IsOwnerOfTheSalonOrIsAdminRequirement>, ISingletonLifetime
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerOfTheSalonOrIsAdminRequirement requirement)
    {
        var currentUserId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var requestedUserId = _httpContextAccessor.HttpContext.Request.RouteValues["userId"]?.ToString();

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}