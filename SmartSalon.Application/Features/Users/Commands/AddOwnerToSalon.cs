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
    IOptions<EmailOptions> _emailOptions,
    IDecryptor _decryptor
) : ICommandHandler<AddOwnerToSalonCommand>
{
    public async Task<Result> Handle(AddOwnerToSalonCommand command, CancellationToken cancellationToken)
    {
        var decryptedToken = _decryptor.DecryptTo<OwnerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey);

        if (decryptedToken is null)
        {
            return new Error("Invalid token");
        }

        var owner = await _owners.GetByIdAsync(decryptedToken.OwnerId);
        var salon = await _salons.All
            .Include(salon => salon.Owners)
            .FirstOrDefaultAsync(salon => salon.Id == decryptedToken.SalonId);

        if (owner is null || salon is null)
        {
            return Error.NotFound;
        }

        salon.Owners!.Add(owner);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
