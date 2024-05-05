using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class DeleteJobTitleCommand(Id specialtyId) : ICommand
{
    public Id JobTitleId => specialtyId;
}

internal class DeleteJobTitleCommandHandler(IEfRepository<JobTitle> _jobTitles, IUnitOfWork _unitOfWork)
    : ICommandHandler<DeleteJobTitleCommand>
{
    public async Task<Result> Handle(DeleteJobTitleCommand command, CancellationToken cancellationToken)
    {
        var jobTitle = await _jobTitles.GetByIdAsync(command.JobTitleId);

        if (jobTitle is null)
        {
            return Error.NotFound;
        }

        await _jobTitles.RemoveByIdAsync(jobTitle.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
