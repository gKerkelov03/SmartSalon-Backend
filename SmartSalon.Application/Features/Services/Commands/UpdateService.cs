
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
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
    public int Order { get; set; }
}

internal class UpdateServiceCommandHandler(
    IEfRepository<Service> _services,
    IJobTitlesRepository _jobTitles,
    IUnitOfWork _unitOfWork
) : ICommandHandler<UpdateServiceCommand>
{
    public async Task<Result> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
    {
        var service = await _services.All
            .Include(service => service.Category)
            .ThenInclude(category => category!.Services)
            .FirstOrDefaultAsync(service => service.Id == command.ServiceId);

        if (service is null)
        {
            return Error.NotFound;
        }

        var categoryAlreadyContainsService = service.Category!.Services!.Any(service => service.Name == command.Name);

        if (categoryAlreadyContainsService)
        {
            return Error.Conflict;
        }

        var jobTitlesResult = _jobTitles.GetJobTitlesInSalon(command.SalonId, command.JobTitlesIds);

        if (jobTitlesResult.IsFailure)
        {
            return jobTitlesResult.Errors!.First();
        }

        service.MapAgainst(command);
        service.JobTitles = jobTitlesResult.Value.ToList();

        _services.Update(service);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
