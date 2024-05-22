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
        var salon = await _salons.All
            .Include(salon => salon.Owners)
            .Include(salon => salon.Workers)
            .Include(salon => salon.JobTitles)
            !.ThenInclude(JobTitle => JobTitle.Workers)
            .FirstOrDefaultAsync(salon => salon.Id == command.SalonId);

        //TODO: if the salon has job titles and workers connected to them it throws exceptions
        if (salon is null)
        {
            return Error.NotFound;
        }

        salon.JobTitles!.ForEach(jobTitle => jobTitle.Workers = []);
        salon!.Workers = null;
        salon!.Owners = null;

        await _salons.RemoveByIdAsync(salon.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(Result.Success());
    }
}
