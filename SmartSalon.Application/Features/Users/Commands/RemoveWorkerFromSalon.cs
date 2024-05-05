using Microsoft.EntityFrameworkCore;
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
        var worker = await _workers.All
            .Include(worker => worker.JobTitles)
            .Include(worker => worker.Salon)
            .Where(worker => worker.Id == command.WorkerId)
            .FirstOrDefaultAsync();

        if (worker is null)
        {
            return Error.NotFound;
        }

        worker.JobTitles = null;
        worker.SalonId = null;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
