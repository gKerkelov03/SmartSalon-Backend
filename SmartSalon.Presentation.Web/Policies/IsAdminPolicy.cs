using Microsoft.AspNetCore.Authorization;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsAdminRequirement : IAuthorizationRequirement { }

internal class IsAdminHandler(ICurrentUserAccessor _currentUser) : AuthorizationHandler<IsAdminRequirement>, IScopedLifetime
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsAdminRequirement requirement)
    {
        if (_currentUser.IsAdmin)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}