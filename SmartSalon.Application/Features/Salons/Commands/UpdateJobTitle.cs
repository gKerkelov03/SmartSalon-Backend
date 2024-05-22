using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class UpdateJobTitleCommand : ICommand
{
    public required Id JobTitleId { get; set; }
    public required string Name { get; set; }
    public required Id SalonId { get; set; }
}

internal class UpdateJobTitleCommandHandler(
    IEfRepository<JobTitle> _jobTitles,
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork
) : ICommandHandler<UpdateJobTitleCommand>
{
    public async Task<Result> Handle(UpdateJobTitleCommand command, CancellationToken cancellationToken)
    {
        var jobTitle = await _jobTitles.GetByIdAsync(command.JobTitleId);

        if (jobTitle is null)
        {
            return Error.NotFound;
        }

        var salon = await _salons.All
            .Include(salon => salon.JobTitles)
            .FirstOrDefaultAsync(salon => salon.Id == command.SalonId);

        if (salon is null)
        {
            return Error.NotFound;
        }

        var salonAlreadyContainsJobTitle = salon.JobTitles!.Any(existingJobTitle => existingJobTitle.Name == existingJobTitle.Name);

        if (salonAlreadyContainsJobTitle)
        {
            return Error.Conflict;
        }

        jobTitle.MapAgainst(command);
        _jobTitles.Update(jobTitle);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
