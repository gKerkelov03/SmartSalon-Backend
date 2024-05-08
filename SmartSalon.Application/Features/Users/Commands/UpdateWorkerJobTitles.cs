using Microsoft.EntityFrameworkCore;
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
    public Id SalonId { get; set; }
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
        var worker = await _workers.All
            .Include(worker => worker.JobTitles)
            .FirstOrDefaultAsync(worker => worker.Id == command.WorkerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        //TODO: This code repeats in the CreateWorker, CreateService and UpdateService as well, think about reusing it
        var jobTitlesFound = _jobTitles.All
            .Where(jobTitle => jobTitle.SalonId == command.SalonId && command.JobTitlesIds.Contains(jobTitle.Id))
            .ToList();

        foreach (var jobTitleId in command.JobTitlesIds)
        {
            var jobTitleNotFound = !jobTitlesFound.Any(jobTitle => jobTitle.Id == jobTitleId);

            if (jobTitleNotFound)
            {
                return Error.NotFound;
            }
        };

        jobTitlesFound.ForEach(jobTitle => worker.JobTitles!.Remove(jobTitle));
        jobTitlesFound.ForEach(jobTitle => worker.JobTitles!.Add(jobTitle));

        _workers.Update(worker);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
