using SmartSalon.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Application.Services;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly ClaimsPrincipal _claimsPrincipal;
    private readonly IEnumerable<string>? _roles;

    public CurrentUserAccessor(IHttpContextAccessor _httpContextAccessor)
    {
        _claimsPrincipal = _httpContextAccessor.HttpContext.User;
        _roles = _claimsPrincipal.FindFirst(ClaimTypes.Role)?.Value.Split(", ");
    }

    public Id Id => _claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value.ToId();
    public bool IsAdmin => _roles!.Contains(AdminRoleName);
    public bool IsOwner => _roles!.Contains(OwnerRoleName);
    public bool IsCustomer => _roles!.Contains(CustomerRoleName);
    public bool IsWorker => _roles!.Contains(WorkerRoleName);
}