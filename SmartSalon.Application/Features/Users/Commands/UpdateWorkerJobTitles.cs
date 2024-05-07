using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Commands;

public class UpdateWorkerJobTitlesCommand : ICommand
{
    public Id WorkerId { get; set; }
    public required IEnumerable<Id> JobTitlesIds { get; set; }
}

internal class UpdateWorkerJobTitlesCommandHandler(
    IEfRepository<Worker> _workers,
    IEfRepository<JobTitle> _jobTitles,
    IUnitOfWork _unitOfWork
) : ICommandHandler<UpdateWorkerJobTitlesCommand>
{
    public async Task<Result> Handle(UpdateWorkerJobTitlesCommand command, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(command.WorkerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        var jobTitlesFound = _jobTitles.All
            .Where(jobTitle => jobTitle.SalonId == worker.SalonId && command.JobTitlesIds.Contains(jobTitle.Id))
            .ToList();

        foreach (var jobTitleId in command.JobTitlesIds)
        {
            var jobTitleNotFound = !jobTitlesFound.Any(jobTitle => jobTitle.Id == jobTitleId);

            if (jobTitleNotFound)
            {
                return Error.NotFound;
            }
        };

        worker.JobTitles = jobTitlesFound;
        _workers.Update(worker);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
