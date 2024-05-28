using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsOwnerOfTheSalonOrIsTheCustomerOrIsTheWorkerOrIsAdminRequirement : IAuthorizationRequirement
{ }

internal class IsOwnerOfTheSalonOrIsTheCustomerOrIsTheWorkerOrIsAdminHandler(
    IHttpContextAccessor _httpContextAccessor,
    ICurrentUserAccessor _currentUser,
    IEfRepository<Salon> _salons
) : AuthorizationHandlerThatNeedsTheRequestBody, IAuthorizationHandler, IScopedLifetime
{
    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        try
        {
            var requirement = GetRequirement<IsOwnerOfTheSalonOrIsTheCustomerOrIsTheWorkerOrIsAdminRequirement>(context);

            if (requirement is null)
            {
                return;
            }

            var requestBodyMap = await GetRequestBodyMapAsync(_httpContextAccessor);

            if (requestBodyMap is null)
            {
                return;
            }

            var requestedCustomerId = requestBodyMap["customerId"].ToString();
            var requestedCustomerIdNotValid = !Id.TryParse(requestedCustomerId, out var customerId);

            var requestedWorkerId = requestBodyMap["workerId"].ToString();
            var requestedWorkerIdNotValid = !Id.TryParse(requestedWorkerId, out var workerId);

            if (requestedCustomerIdNotValid || requestedWorkerIdNotValid)
            {
                return;
            }

            if (_currentUser.IsAdmin || _currentUser.Id.ToString() == requestedCustomerId || _currentUser.Id.ToString() == requestedWorkerId)
            {
                context.Succeed(requirement);
                return;
            }

            var isOwnerOfTheSalon = _salons.All
                .Include(salon => salon.Owners)
                .Any(salon =>
                    salon.Owners!.Any(owner => owner.Id == _currentUser.Id)
                );

            if (isOwnerOfTheSalon)
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