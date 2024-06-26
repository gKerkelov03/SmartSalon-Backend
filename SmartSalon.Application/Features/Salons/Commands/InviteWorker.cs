
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class InviteWorkerCommand : ICommand, IMapTo<WorkerInvitationEncryptionModel>
{
    public Id WorkerId { get; set; }
    public Id SalonId { get; set; }
}

public class InviteWorkerHandler(
    IEfRepository<Worker> _workers,
    IEfRepository<Salon> _salons,
    IEmailsManager _emailsManager,
    IMapper _mapper
) : ICommandHandler<InviteWorkerCommand>
{
    public async Task<Result> Handle(InviteWorkerCommand command, CancellationToken cancellationToken)
    {
        var invitedWorker = await _workers.GetByIdAsync(command.WorkerId);

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
            return new Error("Worker already works in this salon");
        }

        var encryptionModel = _mapper.Map<WorkerInvitationEncryptionModel>(command);

        var viewModel = new WorkerInvitationViewModel
        {
            SalonName = salon.Name,
            WorkerFirstName = invitedWorker.FirstName
        };

        await _emailsManager.SendWorkerInvitationEmailAsync(invitedWorker.Email!, encryptionModel, viewModel);

        return Result.Success();
    }
}