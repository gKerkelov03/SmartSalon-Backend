using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsOwnerOrAdminRequirement : IAuthorizationRequirement { }

internal class IsOwnerOrAdminHandler(IHttpContextAccessor _httpContextAccessor, ICurrentUserAccessor _currentUser)
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