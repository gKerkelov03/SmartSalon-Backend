
using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Commands;

public class CreateSalonCommand : ICommand<CreateSalonCommandResponse>, IMapTo<Salon>
{
    public required string Name { get; set; }
    public required string GoogleMapsLocation { get; set; }
}

public class CreateSalonCommandResponse(Id id)
{
    public Id CreatedSalonId => id;
}

internal class CreateSalonCommandHandler(
    IEfRepository<Salon> _salons,
    IEfRepository<WorkingTime> _workingTimes,
    IEfRepository<Currency> _currencies,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : ICommandHandler<CreateSalonCommand, CreateSalonCommandResponse>
{
    private static object _defaultSalonSettings = new
    {
        BookingsInAdvance = 5,
        TimePenalty = 5,
        SubscriptionsEnabled = true,
        SectionsEnabled = true,
        WorkersCanMoveBookings = true,
        WorkersCanSetNonWorkingPeriods = true,
        WorkersCanDeleteBookings = true,
        Description = "",
        Country = "BULGARIA"
    };

    private static WorkingTime CreateDefaultWorkingTime()
    {
        var startTime = new TimeOnly(9, 0);
        var endTime = new TimeOnly(17, 0);

        var workingTime = new WorkingTime()
        {
            MondayIsWorking = true,
            MondayOpeningTime = startTime,
            MondayClosingTime = endTime,

            TuesdayIsWorking = true,
            TuesdayOpeningTime = startTime,
            TuesdayClosingTime = endTime,

            WednesdayIsWorking = true,
            WednesdayOpeningTime = startTime,
            WednesdayClosingTime = endTime,

            ThursdayIsWorking = true,
            ThursdayOpeningTime = startTime,
            ThursdayClosingTime = endTime,

            FridayIsWorking = true,
            FridayOpeningTime = startTime,
            FridayClosingTime = endTime,

            SaturdayIsWorking = true,
            SaturdayOpeningTime = startTime,
            SaturdayClosingTime = endTime,

            SundayIsWorking = true,
            SundayOpeningTime = startTime,
            SundayClosingTime = endTime
        };

        return workingTime;
    }

    public async Task<Result<CreateSalonCommandResponse>> Handle(CreateSalonCommand command, CancellationToken cancellationClosingTimeken)
    {
        //TODO: this default currency can be cached but I don't know how would be the best to do it because the handler is transient
        var defaultCurrency = _currencies.FirstOrDefault(currency => currency.Code == "BGN");

        var newSalon = _mapper.Map<Salon>(command);
        var workingTime = CreateDefaultWorkingTime();

        newSalon.MapAgainst(_defaultSalonSettings);
        newSalon.MainCurrency = defaultCurrency;
        newSalon.WorkingTimeId = workingTime.Id;
        workingTime.SalonId = newSalon.Id;

        await _salons.AddAsync(newSalon);
        await _workingTimes.AddAsync(workingTime);
        await _unitOfWork.SaveChangesAsync(cancellationClosingTimeken);

        return new CreateSalonCommandResponse(newSalon.Id);
    }
}
