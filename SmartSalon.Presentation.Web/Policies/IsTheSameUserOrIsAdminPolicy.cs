using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsTheSameUserOrIsAdminRequirement : IAuthorizationRequirement { }

internal class IsTheSameUserOrIsAdminHandler(IHttpContextAccessor _httpContextAccessor, ICurrentUserAccessor _currentUser)
    : AuthorizationHandler<IsTheSameUserOrIsAdminRequirement>, IScopedLifetime
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsTheSameUserOrIsAdminRequirement requirement)
    {
        var idRouteParameterName = IdRoute.Remove(['{', '}']);
        var requestedUserId = _httpContextAccessor.HttpContext?.Request.RouteValues[idRouteParameterName]?.ToString();

        if (_currentUser.IsAdmin || requestedUserId == _currentUser.Id.ToString())
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}