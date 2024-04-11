using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.Options;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class AddOwnerToSalonCommand(string token) : ICommand
{
    public string Token => token;
}

internal class AddOwnerToSalonCommandHandler(
    IEfRepository<Owner> _owners,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork,
    IOptions<EmailsOptions> _emailOptions,
    IEncryptionHelper _encryptionHelper
) : ICommandHandler<AddOwnerToSalonCommand>
{
    public async Task<Result> Handle(AddOwnerToSalonCommand command, CancellationToken cancellationToken)
    {
        var encryptionModel = _encryptionHelper.DecryptTo<OwnerInvitationEmailEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey);

        if (encryptionModel is null)
        {
            return new Error("Invalid token");
        }

        var owner = await _owners.GetByIdAsync(encryptionModel.OwnerId);
        var salon = await _salons.All
            .Where(salon => salon.Id == encryptionModel.SalonId)
            .Include(salon => salon.Owners)
            .FirstOrDefaultAsync();

        if (owner is null || salon is null)
        {
            return Error.NotFound;
        }

        salon.Owners!.Add(owner);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
