using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Domain.Salons;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsOwnerOfTheSalonOfTheWorkerOrIsAdminRequirement : IAuthorizationRequirement { }

internal class IsOwnerOfTheSalonOfTheWorkerOrIsAdminHandler(
    IHttpContextAccessor _httpContextAccessor,
    ICurrentUserAccessor _currentUser,
    IEfRepository<Salon> _salons
) : AuthorizationHandlerThatNeedsTheRequestBody, IAuthorizationHandler, IScopedLifetime
{
    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        try
        {
            var requirement = GetRequirement<IsOwnerOfTheSalonOfTheWorkerOrIsAdminRequirement>(context);

            if (requirement is null)
            {
                return;
            }

            var requestBodyMap = await GetRequestBodyMapAsync(_httpContextAccessor);

            if (requestBodyMap is null)
            {
                return;
            }

            var requestedWorkerId = requestBodyMap["workerId"].ToString();
            var requestedWorkerIdNotValid = !Id.TryParse(requestedWorkerId, out var workerId);

            if (requestedWorkerIdNotValid)
            {
                return;
            }

            if (_currentUser.IsAdmin)
            {
                context.Succeed(requirement);
                return;
            }

            var isOwnerOfTheSalon = _salons.All
                .Include(salon => salon.Owners)
                .Include(salon => salon.Workers)
                .Any(salon =>
                    salon.Owners!.Any(owner => owner.Id == _currentUser.Id) &&
                    salon.Workers!.Any(worker => worker.Id.ToString() == requestedWorkerId)
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