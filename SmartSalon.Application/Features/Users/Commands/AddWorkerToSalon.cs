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
    IOptions<EmailsOptions> _emailOptions,
    IEncryptionHelper _encryptionHelper
)
    : ICommandHandler<AddWorkerToSalonCommand>
{
    public async Task<Result> Handle(AddWorkerToSalonCommand command, CancellationToken cancellationToken)
    {
        var encryptionModel = _encryptionHelper.DecryptTo<WorkerInvitationEmailEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey);

        if (encryptionModel is null)
        {
            return new Error("Invalid token");
        }

        var worker = await _workers.GetByIdAsync(encryptionModel.WorkerId);
        var salon = await _salons.All
            .Where(salon => salon.Id == encryptionModel.SalonId)
            .Include(salon => salon.Workers)
            .FirstOrDefaultAsync();

        if (worker is null || salon is null)
        {
            return Error.NotFound;
        }

        salon!.Workers!.Add(worker);
        await _unitOfWork.SaveAsync(cancellationToken);

        return Result.Success();
    }
}
