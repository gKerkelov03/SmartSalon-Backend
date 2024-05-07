﻿
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
    public required string Location { get; set; }
}

public class CreateSalonCommandResponse(Id id)
{
    public Id CreatedSalonId => id;
}

internal class CreateSalonCommandHandler(
    IEfRepository<Salon> _salons,
    IEfRepository<Currency> _currencies,
    IUnitOfWork _unitOfWork,
    IMapper _mapper
) : ICommandHandler<CreateSalonCommand, CreateSalonCommandResponse>
{
    private static object _defaultSalonSettings = new
    {
        WorkingTimeId = CreateDefaultWorkingTime().Id,
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
            MondayOpeningTime = startTime,
            MondayClosingTime = endTime,

            TuesdayOpeningTime = startTime,
            TuesdayClosingTime = endTime,

            WednesdayOpeningTime = startTime,
            WednesdayClosingTime = endTime,

            ThursdayOpeningTime = startTime,
            ThursdayClosingTime = endTime,

            FridayOpeningTime = startTime,
            FridayClosingTime = endTime,

            SaturdayOpeningTime = startTime,
            SaturdayClosingTime = endTime,

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
        newSalon.MapAgainst(_defaultSalonSettings);
        newSalon.MainCurrency = defaultCurrency;

        await _salons.AddAsync(newSalon);
        await _unitOfWork.SaveChangesAsync(cancellationClosingTimeken);

        return new CreateSalonCommandResponse(newSalon.Id);
    }
}
