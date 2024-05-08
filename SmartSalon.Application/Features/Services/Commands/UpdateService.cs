
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartService.Application.Features.Services.Commands;

public class UpdateServiceCommand : ICommand
{
    public required Id ServiceId { get; set; }
    public required Id SalonId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public required IEnumerable<Id> JobTitlesIds { get; set; }
}

internal class UpdateServiceCommandHandler(
    IEfRepository<Service> _services,
    IEfRepository<JobTitle> _jobTitles,
    IUnitOfWork _unitOfWork
) : ICommandHandler<UpdateServiceCommand>
{
    public async Task<Result> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
    {
        var service = await _services.GetByIdAsync(command.ServiceId);

        if (service is null)
        {
            return Error.NotFound;
        }

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

        service.MapAgainst(command);
        service.JobTitles = jobTitlesFound;

        _services.Update(service);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
