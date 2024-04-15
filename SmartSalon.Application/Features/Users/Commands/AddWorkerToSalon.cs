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

public class AddWorkerToSalonCommand(string token) : ICommand
{
    public string Token => token;
}

internal class AddWorkerToSalonCommandHandler(
    IEfRepository<Worker> _workers,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork,
    IOptions<EmailOptions> _emailOptions,
    IEncryptionHelper _encryptionHelper
) : ICommandHandler<AddWorkerToSalonCommand>
{
    public async Task<Result> Handle(AddWorkerToSalonCommand command, CancellationToken cancellationToken)
    {
        var decryptedToken = _encryptionHelper.DecryptTo<WorkerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey);

        if (decryptedToken is null)
        {
            return new Error("Invalid token");
        }

        var worker = await _workers.GetByIdAsync(decryptedToken.WorkerId);
        var salon = await _salons.All
            .Where(salon => salon.Id == decryptedToken.SalonId)
            .Include(salon => salon.Workers)
            .FirstOrDefaultAsync();

        if (worker is null || salon is null)
        {
            return Error.NotFound;
        }

        salon!.Workers!.Add(worker);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
