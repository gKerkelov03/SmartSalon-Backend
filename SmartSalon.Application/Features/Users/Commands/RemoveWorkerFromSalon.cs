﻿using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class RemoveWorkerFromSalonCommand(Id workerId) : ICommand
{
    public Id WorkerId => workerId;
}

internal class RemoveWorkerFromSalonCommandHandler(IEfRepository<Worker> _workers, IEfRepository<Salon> _salons, IUnitOfWork _unitOfWork)
    : ICommandHandler<RemoveWorkerFromSalonCommand>
{
    public async Task<Result> Handle(RemoveWorkerFromSalonCommand command, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(command.WorkerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        var salon = await _salons.All
            .Where(salon => salon.Id == worker.SalonId)
            .Include(salon => salon.Workers)
            .FirstOrDefaultAsync();

        if (salon is null)
        {
            return Error.NotFound;
        }

        salon.Workers!.Remove(worker);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
