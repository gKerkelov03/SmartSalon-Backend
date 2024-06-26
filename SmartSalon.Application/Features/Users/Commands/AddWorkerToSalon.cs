﻿using Microsoft.EntityFrameworkCore;
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
    IDecryptor _decryptor
) : ICommandHandler<AddWorkerToSalonCommand>
{
    public async Task<Result> Handle(AddWorkerToSalonCommand command, CancellationToken cancellationToken)
    {
        var decryptedToken = _decryptor.DecryptTo<WorkerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey);

        if (decryptedToken is null)
        {
            return new Error("Invalid token");
        }

        var worker = await _workers.GetByIdAsync(decryptedToken.WorkerId);
        var salon = await _salons.All
            .Include(salon => salon.Workers)
            .FirstOrDefaultAsync(salon => salon.Id == decryptedToken.SalonId);

        if (worker is null || salon is null)
        {
            return Error.NotFound;
        }

        //TODO: this check is actually unneeded because of how EF core works
        var workerIsNotAlreadyInTheSalon = !salon.Workers!.Any(worker => worker.Id == decryptedToken.WorkerId);
        if (workerIsNotAlreadyInTheSalon)
        {
            salon!.Workers!.Add(worker);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Result.Success();
    }
}
