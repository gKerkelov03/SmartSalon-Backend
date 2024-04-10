using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerRequirement : IAuthorizationRequirement { }

internal class IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerHandler(IHttpContextAccessor _httpContextAccessor) : AuthorizationHandler<IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerRequirement>, ISingletonLifetime
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerRequirement requirement)
    {
        var currentUserId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var requestedUserId = _httpContextAccessor.HttpContext.Request.RouteValues["userId"]?.ToString();

        context.Succeed(requirement);

        return Task.CompletedTask;
    }
}