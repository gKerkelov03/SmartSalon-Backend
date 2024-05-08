using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Services.Commands;

public class CreateServiceCommand : ICommand<CreateServiceCommandResponse>, IMapTo<Service>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double Price { get; set; }
    public required int DurationInMinutes { get; set; }
    public Id CategoryId { get; set; }
    public Id SalonId { get; set; }
    public required IEnumerable<Id> JobTitlesIds { get; set; }
}

public class CreateServiceCommandResponse(Id id)
{
    public Id CreatedServiceId => id;
}

internal class CreateServiceCommandHandler(
    IEfRepository<Category> _categories,
    IEfRepository<Service> _services,
    IEfRepository<JobTitle> _jobTitles,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : ICommandHandler<CreateServiceCommand, CreateServiceCommandResponse>
{
    public async Task<Result<CreateServiceCommandResponse>> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
    {
        var newService = _mapper.Map<Service>(command);

        var category = await _categories.All
            .Include(category => category.Services)
            .FirstOrDefaultAsync(category => category.Id == command.CategoryId);

        if (category is null)
        {
            return Error.NotFound;
        }

        var categoryAlreadyContainsService = category.Services!.Any(service => service.Name == newService.Name);

        if (categoryAlreadyContainsService)
        {
            return Error.Conflict;
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

        newService.Order = _services.All.Max(section => section.Order) + 1;
        newService.JobTitles = jobTitlesFound;

        //TODO: debug why this throws error, expected one row to be added but 0 were added
        //category.Services!.Add(newService);

        await _services.AddAsync(newService);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateServiceCommandResponse(newService.Id);
    }
}
