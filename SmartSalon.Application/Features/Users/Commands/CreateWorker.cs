using AutoMapper;
using SmartSalon.Application.Abstractions;
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
    IUnitOfWork _unitOfWork,
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


        var jobTitlesFound = _jobTitles.All
            .Where(jobTitle => jobTitle.SalonId == command.SalonId && command.JobTitlesIds.Contains(jobTitle.Id))
            .ToList();

        foreach (var jobTitleId in command.JobTitlesIds)
        {
            var jobTitleNotFound = !jobTitlesFound.Any(jobTitle => jobTitle.Id == jobTitleId);

            if (jobTitleNotFound)
            {
                return Error.NotFound;
            }
        };

        var newWorker = _mapper.Map<Worker>(command);

        newWorker.UserName = command.Email;
        newWorker.JobTitles = jobTitlesFound;
        newWorker.Nickname = $"{newWorker.FirstName} {newWorker.LastName}";
        newWorker.Salons = [salon];

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
