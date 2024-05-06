using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class CreateJobTitleCommand : ICommand<CreateJobTitleCommandResponse>, IMapTo<JobTitle>
{
    public required Id SalonId { get; set; }
    public required string Name { get; set; }
}

public class CreateJobTitleCommandResponse(Id id)
{
    public Id CreatedJobTitleId => id;
}

internal class CreateJobTitleCommandHandler(
    IEfRepository<Salon> _salons,
    IEfRepository<JobTitle> _jobTitles,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
)
    : ICommandHandler<CreateJobTitleCommand, CreateJobTitleCommandResponse>
{
    public async Task<Result<CreateJobTitleCommandResponse>> Handle(CreateJobTitleCommand command, CancellationToken cancellationToken)
    {
        var newJobTitle = _mapper.Map<JobTitle>(command);

        var salon = await _salons.All
            .Include(salon => salon.JobTitles)
            .Where(salon => salon.Id == command.SalonId)
            .FirstOrDefaultAsync();

        if (salon is null)
        {
            return Error.NotFound;
        }

        var salonAlreadyContainsJobTitle = salon.JobTitles!.Any(jobTitle => jobTitle.Name == newJobTitle.Name);

        if (salonAlreadyContainsJobTitle)
        {
            return Error.Conflict;
        }

        //TODO: debug why this throws error, expected one row to be added but 0 were added
        // salon.JobTitles!.Add(newJobTitle);

        await _jobTitles.AddAsync(newJobTitle);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateJobTitleCommandResponse(newJobTitle.Id);
    }
}
