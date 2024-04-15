
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

public class SendOwnerInvitationEmailCommand : ICommand, IMapTo<OwnerInvitationEncryptionModel>
{
    public Id OwnerId { get; set; }
    public Id SalonId { get; set; }
}

public class SendOwnerInvitationEmailHandler(
    UsersManager _usersManager,
    IEfRepository<Salon> _salons,
    IEmailsManager _emailsManager,
    IMapper _mapper
) : ICommandHandler<SendOwnerInvitationEmailCommand>
{
    public async Task<Result> Handle(SendOwnerInvitationEmailCommand command, CancellationToken cancellationToken)
    {
        var invitedOwner = await _usersManager.FindByIdAsync(command.OwnerId.ToString());

        if (invitedOwner is null)
        {
            return Error.NotFound;
        }

        var salon = await _salons.GetByIdAsync(command.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        var invitedOwnerIsAlreadyInTheSalon = _salons.All
            .Include(salon => salon.Owners)
            .Any(
                salon => salon.Id == command.SalonId &&
                salon.Owners!.Any(owner => owner.Id == invitedOwner.Id)
            );

        if (invitedOwnerIsAlreadyInTheSalon)
        {
            return new Error("Owner already owns the salon");
        }

        var encryptionModel = _mapper.Map<OwnerInvitationEncryptionModel>(command);

        var viewModel = new OwnerInvitationViewModel
        {
            OwnerFirstName = invitedOwner.FirstName,
            SalonName = salon.Name
        };

        await _emailsManager.SendOwnerInvitationEmailAsync(invitedOwner.Email!, encryptionModel, viewModel);

        return Result.Success();
    }
}