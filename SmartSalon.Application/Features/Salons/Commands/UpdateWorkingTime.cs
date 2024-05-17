﻿using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartWorkingTime.Application.Features.Salons.Commands;

public class UpdateWorkingTimeCommand : ICommand
{
    public required Id WorkingTimeId { get; set; }
    public required DayOfWeek DayOfWeek { get; set; }
    public required TimeOnly StartTime { get; set; }
    public required TimeOnly EndTime { get; set; }
    public required bool IsWorking { get; set; }
}

internal class UpdateWorkingTimeCommandHandler(IEfRepository<WorkingTime> _workingTimes, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateWorkingTimeCommand>
{
    public async Task<Result> Handle(UpdateWorkingTimeCommand command, CancellationToken cancellationClosingTimeken)
    {
        var workingTime = await _workingTimes.GetByIdAsync(command.WorkingTimeId);

        if (workingTime is null)
        {
            return Error.NotFound;
        }

        UpdateWorkingTimeForDayOfWeek(workingTime, command.DayOfWeek, command.IsWorking, command.StartTime, command.EndTime);
        _workingTimes.Update(workingTime);

        await _unitOfWork.SaveChangesAsync(cancellationClosingTimeken);

        return Result.Success();
    }

    private void UpdateWorkingTimeForDayOfWeek(
        WorkingTime workingTime,
        DayOfWeek dayOfWeek,
        bool IsWorking,
        TimeOnly newOpeningTime,
        TimeOnly newClosingTime
    )
    {
        switch (dayOfWeek)
        {
            case DayOfWeek.Monday:
                workingTime.MondayIsWorking = IsWorking;
                workingTime.MondayOpeningTime = newOpeningTime;
                workingTime.MondayClosingTime = newClosingTime;
                break;
            case DayOfWeek.Tuesday:
                workingTime.TuesdayIsWorking = IsWorking;
                workingTime.TuesdayOpeningTime = newOpeningTime;
                workingTime.TuesdayClosingTime = newClosingTime;
                break;
            case DayOfWeek.Wednesday:
                workingTime.WednesdayIsWorking = IsWorking;
                workingTime.WednesdayOpeningTime = newOpeningTime;
                workingTime.WednesdayClosingTime = newClosingTime;
                break;
            case DayOfWeek.Thursday:
                workingTime.ThursdayIsWorking = IsWorking;
                workingTime.ThursdayOpeningTime = newOpeningTime;
                workingTime.ThursdayClosingTime = newClosingTime;
                break;
            case DayOfWeek.Friday:
                workingTime.FridayIsWorking = IsWorking;
                workingTime.FridayOpeningTime = newOpeningTime;
                workingTime.FridayClosingTime = newClosingTime;
                break;
            case DayOfWeek.Saturday:
                workingTime.SaturdayIsWorking = IsWorking;
                workingTime.SaturdayOpeningTime = newOpeningTime;
                workingTime.SaturdayClosingTime = newClosingTime;
                break;
            case DayOfWeek.Sunday:
                workingTime.SundayIsWorking = IsWorking;
                workingTime.SundayOpeningTime = newOpeningTime;
                workingTime.SundayClosingTime = newClosingTime;
                break;
        }
    }
}