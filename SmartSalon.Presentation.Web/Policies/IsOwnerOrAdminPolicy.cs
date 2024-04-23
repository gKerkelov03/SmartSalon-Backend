using Microsoft.AspNetCore.Authorization;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsOwnerOrAdminRequirement : IAuthorizationRequirement { }

internal class IsOwnerOrAdminHandler(ICurrentUserAccessor _currentUser)
    : AuthorizationHandler<IsOwnerOrAdminRequirement>, IScopedLifetime
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerOrAdminRequirement requirement)
    {
        if (_currentUser.IsOwner || _currentUser.IsAdmin)
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}