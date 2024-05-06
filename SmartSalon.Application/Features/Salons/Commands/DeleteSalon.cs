using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class DeleteSalonCommand(Id id) : ICommand
{
    public Id SalonId => id;
}

internal class DeleteSalonCommandHandler(IEfRepository<Salon> _salons, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteSalonCommand>
{
    public async Task<Result> Handle(DeleteSalonCommand command, CancellationToken cancellationToken)
    {
        //TODO: check why the commented code produces errors and remove the job titles of a worker when you delete the salon
        var salon = await _salons.All
            .Include(salon => salon.Owners)
            .Include(salon => salon.Workers)
            // !.ThenInclude(worker => worker.JobTitles)
            .Where(salon => salon.Id == command.SalonId)
            .FirstOrDefaultAsync();

        // salon!.Workers!.ForEach(worker => worker.JobTitles = null);
        salon!.Workers = null;
        salon!.Owners = null;

        if (salon is null)
        {
            return Error.NotFound;
        }

        await _salons.RemoveByIdAsync(salon.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Result.Success());
    }
}
