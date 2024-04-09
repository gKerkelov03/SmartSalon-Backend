using SmartSalon.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Application.Services;

public class CurrentUserAccessor(IHttpContextAccessor _httpContextAccessor) : ICurrentUserAccessor
{
    public Id Id
    {
        get
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (currentUserId is null)
            {
                throw new InvalidOperationException("There is no logged in user. CurrentUserId is null when it shouldn't be.");
            }

            return currentUserId.Value.ToId();
        }
    }
}