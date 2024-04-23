
using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class CreateSalonCommand : ICommand<CreateSalonCommandResponse>, IMapTo<Salon>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string Location { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required int DefaultTimePenalty { get; set; }
    public required int DefaultBookingsInAdvance { get; set; }
    public bool SubscriptionsEnabled { get; set; }
    public bool SectionsEnabled { get; set; }
    public bool WorkersCanMoveBookings { get; set; }
    public bool WorkersCanSetNonWorkingPeriods { get; set; }
}

public class CreateSalonCommandResponse(Id id)
{
    public Id CreatedSalonId => id;
}

internal class CreateSalonCommandHandler(
    IEfRepository<Salon> _salons,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : ICommandHandler<CreateSalonCommand, CreateSalonCommandResponse>
{
    public async Task<Result<CreateSalonCommandResponse>> Handle(CreateSalonCommand command, CancellationToken cancellationToken)
    {
        var salon = _mapper.Map<Salon>(command);
        salon.WorkingTime = GetDefaultWorkingTime();

        await _salons.AddAsync(salon);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateSalonCommandResponse(salon.Id);
    }

    private WorkingTime GetDefaultWorkingTime()
    {
        //From nine to five all days
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);

        return new WorkingTime()
        {
            MondayFrom = startTime,
            MondayTo = endTime,

            TuesdayFrom = startTime,
            TuesdayTo = endTime,

            WednesdayFrom = startTime,
            WednesdayTo = endTime,

            ThursdayFrom = startTime,
            ThursdayTo = endTime,

            FridayFrom = startTime,
            FridayTo = endTime,

            SaturdayFrom = startTime,
            SaturdayTo = endTime,

            SundayFrom = startTime,
            SundayTo = endTime
        };
    }
}
