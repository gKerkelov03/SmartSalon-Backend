using AutoMapper;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class CreateWorkerCommand : ICommand<CreateWorkerCommandResponse>, IMapTo<Worker>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Nickname { get; set; }
    public required string PhoneNumber { get; set; }
    public required string JobTitle { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public Id SalonId { get; set; }
}

public class CreateWorkerCommandResponse(Id createdWorkerId)
{
    public Id CreatedWorkerId => createdWorkerId;
}

internal class CreateWorkerCommandHandler(UsersManager _users, IEfRepository<Salon> _salons, IMapper _mapper)
    : ICommandHandler<CreateWorkerCommand, CreateWorkerCommandResponse>
{
    public async Task<Result<CreateWorkerCommandResponse>> Handle(CreateWorkerCommand command, CancellationToken cancellationToken)
    {
        var userWithTheSameEmail = await _users.FindByEmailAsync(command.Email);

        if (userWithTheSameEmail is not null)
        {
            return Error.Conflict;
        }

        var salonWithThisId = await _salons.GetByIdAsync(command.SalonId);

        if (salonWithThisId is null)
        {
            return Error.NotFound;
        }

        var worker = _mapper.Map<Worker>(command);
        worker.UserName = command.Email;

        var identityResultForCreation = await _users.CreateAsync(worker);
        var identityResultForAddingToRole = await _users.AddToRoleAsync(worker, WorkerRoleName);

        if (identityResultForCreation.Failure())
        {
            return new Error(identityResultForCreation.ErrorDescription());
        }

        if (identityResultForAddingToRole.Failure())
        {
            return new Error(identityResultForAddingToRole.ErrorDescription());
        }

        return new CreateWorkerCommandResponse(worker.Id);
    }
}
