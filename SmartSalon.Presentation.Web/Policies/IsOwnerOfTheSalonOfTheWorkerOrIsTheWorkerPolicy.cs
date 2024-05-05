using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Presentation.Web.Policies;

internal class IsOwnerOfTheSalonOfTheWorkerOrIsAdminRequirement : IAuthorizationRequirement { }

internal class IsOwnerOfTheSalonOfTheWorkerOrIsAdminHandler(
    IHttpContextAccessor _httpContextAccessor,
    ICurrentUserAccessor _currentUser,
    IEfRepository<Worker> _workers,
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

            var requestedWorkerId = requestBodyMap["workerId"];
            var requestedWorkerIdNotValid = !Id.TryParse(requestedWorkerId, out var workerId);

            if (requestedWorkerIdNotValid)
            {
                return;
            }

            var salon = await _workers.All
                .Include(worker => worker.Salon)
                .Where(worker => worker.Id == workerId)
                .FirstOrDefaultAsync();

            if (salon is null)
            {
                return;
            }

            var salonId = salon.Id;
            var isOwnerOfTheSalon = _salons
                .All
                .Include(salon => salon.Owners)
                .Include(salon => salon.Workers)
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