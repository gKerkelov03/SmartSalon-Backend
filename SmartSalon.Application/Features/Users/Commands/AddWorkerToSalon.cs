using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class AddWorkerToSalonCommand : ICommand
{
    public Id SalonId { get; set; }
    public Id WorkerId { get; set; }
}

internal class AddWorkerToSalonCommandHandler(IEfRepository<Worker> _workers, IEfRepository<Salon> _salons, IUnitOfWork _unitOfWork)
    : ICommandHandler<AddWorkerToSalonCommand>
{
    public async Task<Result> Handle(AddWorkerToSalonCommand command, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(command.WorkerId);
        var salon = await _salons.All
            .Where(salon => salon.Id == command.SalonId)
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
