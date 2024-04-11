
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class SendWorkerInvitationEmailCommand : ICommand, IMapTo<WorkerInvitationEmailEncryptionModel>
{
    public Id WorkerId { get; set; }
    public Id SalonId { get; set; }
}

public class SendWorkerInvitationEmailHandler(
    UsersManager _usersManager,
    IEfRepository<Salon> _salons,
    IEmailsManager _emailsManager,
    IMapper _mapper
) : ICommandHandler<SendWorkerInvitationEmailCommand>
{
    public async Task<Result> Handle(SendWorkerInvitationEmailCommand command, CancellationToken cancellationToken)
    {
        var invitedWorker = await _usersManager.FindByIdAsync(command.WorkerId.ToString());

        if (invitedWorker is null)
        {
            return Error.NotFound;
        }

        var salon = await _salons.GetByIdAsync(command.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        var invitedWorkerIsAlreadyInTheSalon = _salons.All
            .Include(salon => salon.Workers)
            .Any(
                salon => salon.Id == command.SalonId &&
                salon.Workers!.Any(worker => worker.Id == invitedWorker.Id)
            );

        if (invitedWorkerIsAlreadyInTheSalon)
        {
            return new Error("Worker already included in the workers of the salon");
        }

        var encryptionModel = _mapper.Map<WorkerInvitationEmailEncryptionModel>(command);

        var viewModel = new WorkerInvitationEmailViewModel
        {
            SalonName = salon.Name,
            WorkerFirstName = invitedWorker.FirstName
        };

        await _emailsManager.SendWorkerInvitationEmailAsync(invitedWorker.Email!, encryptionModel, viewModel);

        return Result.Success();
    }
}