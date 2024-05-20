
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
    IMapper _mapper,
    IGeolocator _geolocator
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
        Description = ""
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
        var coordinatesResult = await _geolocator.GetCoordinatesAsync(command.GoogleMapsLocation);

        if (coordinatesResult.IsFailure)
        {
            return coordinatesResult.Errors!.First();
        }

        var defaultCurrency = _currencies.FirstOrDefault(currency => currency.Code == "BGN");
        var newWorkingTime = CreateDefaultWorkingTime();

        var newSalon = _mapper.Map<Salon>(command);
        newSalon.MapAgainst(_defaultSalonSettings);

        newSalon.MainCurrency = defaultCurrency;
        newSalon.WorkingTimeId = newWorkingTime.Id;
        newSalon.Latitude = coordinatesResult.Value.Latitude;
        newSalon.Longitude = coordinatesResult.Value.Longitude;
        newSalon.Country = coordinatesResult.Value.Country.ToUpper();

        newWorkingTime.SalonId = newSalon.Id;

        await _salons.AddAsync(newSalon);
        await _workingTimes.AddAsync(newWorkingTime);
        await _unitOfWork.SaveChangesAsync(cancellationClosingTimeken);

        return new CreateSalonCommandResponse(newSalon.Id);
    }
}
