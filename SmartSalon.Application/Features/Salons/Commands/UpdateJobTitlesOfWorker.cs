using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class UpdateJobTitlesOfWorkerCommand : ICommand
{
    public required Id WorkerId { get; set; }
    public required Id SalonId { get; set; }
    public required IEnumerable<Id> JobTitlesIds { get; set; }
}

public class UpdateJobTitlesOfWorkerCommandResponse : ICommand
{
    public required Id SalonId { get; set; }
    public required string Text { get; set; }
}

internal class UpdateJobTitlesOfWorkerCommandHandler(
    IEfRepository<JobTitle> _jobTitles,
    IEfRepository<Worker> _workers,
    IUnitOfWork _unitOfWork
) : ICommandHandler<UpdateJobTitlesOfWorkerCommand>
{
    public async Task<Result> Handle(UpdateJobTitlesOfWorkerCommand command, CancellationToken cancellationToken)
    {
        var worker = await _workers.All
            .Include(worker => worker.JobTitles)
            .Where(worker => worker.Id == command.WorkerId)
            .FirstOrDefaultAsync();

        if (worker is null)
        {
            return Error.NotFound;
        }

        worker.JobTitles = _jobTitles.All
            .Where(jobTitle => jobTitle.SalonId == command.SalonId && command.JobTitlesIds.Contains(jobTitle.Id))
            .ToList();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
