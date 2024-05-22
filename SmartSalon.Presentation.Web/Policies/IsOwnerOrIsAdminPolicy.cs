using Microsoft.AspNetCore.Authorization;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsOwnerOrIsAdminRequirement : IAuthorizationRequirement { }

internal class IsOwnerOrIsAdminHandler(ICurrentUserAccessor _currentUser)
    : AuthorizationHandler<IsOwnerOrIsAdminRequirement>, IScopedLifetime
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerOrIsAdminRequirement requirement)
    {
        if (_currentUser.IsOwner || _currentUser.IsAdmin)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}