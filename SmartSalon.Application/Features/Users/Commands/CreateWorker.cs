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
    public required string ProfilePictureUrl { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public Id SalonId { get; set; }
    public required IEnumerable<Id> JobTitlesIds { get; set; }
}

public class CreateWorkerCommandResponse(Id createdWorkerId)
{
    public Id CreatedWorkerId => createdWorkerId;
}

internal class CreateWorkerCommandHandler(
    UsersManager _users,
    IEfRepository<Salon> _salons,
    IEfRepository<JobTitle> _jobTitles,
    IMapper _mapper
)
    : ICommandHandler<CreateWorkerCommand, CreateWorkerCommandResponse>
{
    public async Task<Result<CreateWorkerCommandResponse>> Handle(CreateWorkerCommand command, CancellationToken cancellationToken)
    {
        var userWithTheSameEmail = await _users.FindByEmailAsync(command.Email);

        if (userWithTheSameEmail is not null)
        {
            return Error.Conflict;
        }

        var salon = await _salons.GetByIdAsync(command.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        //TODO: remember you set EmailConfirmed to true, you might want to change that in the future
        var newWorker = _mapper.Map<Worker>(command);
        newWorker.UserName = command.Email;
        newWorker.JobTitles = (await _jobTitles.FindAllAsync(jobTitle => command.JobTitlesIds.Contains(jobTitle.Id))).ToList();
        newWorker.EmailConfirmed = true;

        var identityResultForCreation = await _users.CreateAsync(newWorker, command.Password);
        var identityResultForAddingToRole = await _users.AddToRoleAsync(newWorker, WorkerRoleName);

        if (identityResultForCreation.Failure())
        {
            return new Error(identityResultForCreation.ErrorDescription());
        }

        if (identityResultForAddingToRole.Failure())
        {
            return new Error(identityResultForAddingToRole.ErrorDescription());
        }

        return new CreateWorkerCommandResponse(newWorker.Id);
    }
}
