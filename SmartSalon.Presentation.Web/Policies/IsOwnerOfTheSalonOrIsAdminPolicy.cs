using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsOwnerOfTheSalonOrIsAdminRequirement : IAuthorizationRequirement { }

internal class IsOwnerOfTheSalonOrIsAdminHandler(
    IHttpContextAccessor _httpContextAccessor,
    ICurrentUserAccessor _currentUser,
    IEfRepository<Salon> _salons
) : AuthorizationHandlerThatNeedsTheRequestBody, IAuthorizationHandler, IScopedLifetime
{
    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        try
        {
            var requirement = GetRequirement<IsOwnerOfTheSalonOrIsAdminRequirement>(context);
            var passedIdRouteParameter = _httpContextAccessor.HttpContext?.Request.RouteValues[IdRouteParameterName]?.ToString();
            var salonIdPropertyName = "salonId";

            if (requirement is null)
            {
                return;
            }

            var requestBodyMap = await GetRequestBodyMapAsync(_httpContextAccessor);

            if (requestBodyMap is null)
            {
                return;
            }

            var requestedSalonId = passedIdRouteParameter;

            if (requestBodyMap.ContainsKey(salonIdPropertyName))
            {
                requestedSalonId = requestBodyMap[salonIdPropertyName].ToString();
            }
            else if (passedIdRouteParameter is null)
            {
                return;
            }

            var requestedSalonIdNotValid = !Id.TryParse(requestedSalonId, out var salonId);

            if (requestedSalonIdNotValid)
            {
                return;
            }

            var isOwnerOfTheSalon = _salons
                .All
                .Include(salon => salon.Owners)
                .Where(salon => salon.Id == salonId)
                .Any(salon => salon.Owners!.Any(owner => owner.Id == _currentUser.Id));

            if (_currentUser.IsAdmin || isOwnerOfTheSalon)
            {
                context.Succeed(requirement);
            }
        }
        catch
        {
            return;
        }
    }
}