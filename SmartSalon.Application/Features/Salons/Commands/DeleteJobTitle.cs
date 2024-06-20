using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class DeleteJobTitleCommand() : ICommand
{
    public Id JobTitleId { get; set; }
}

internal class DeleteJobTitleCommandHandler(IEfRepository<JobTitle> _jobTitles, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteJobTitleCommand>
{
    public async Task<Result> Handle(DeleteJobTitleCommand command, CancellationToken cancellationToken)
    {
        var jobTitle = await _jobTitles.All
            .Include(jobTitle => jobTitle.Workers)
            .FirstOrDefaultAsync(jobTitle => jobTitle.Id == command.JobTitleId);

        if (jobTitle is null)
        {
            return Error.NotFound;
        }

        if (jobTitle.Workers!.Count() > 0)
        {
            var firstWorker = jobTitle.Workers!.First();
            var firstWorkerName = $"{firstWorker.FirstName} {firstWorker.FirstName}";

            return new Error($"Cannot delete {jobTitle.Name} because {firstWorkerName} is connected to it");
        }

        await _jobTitles.RemoveByIdAsync(jobTitle.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
