using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartWorkingTime.Application.Features.Salons.Commands;

public class UpdateWorkingTimeCommand : ICommand
{
    public required Id WorkingTimeId { get; set; }
    public required DayOfWeek DayOfWeek { get; set; }
    public required TimeOnly From { get; set; }
    public required TimeOnly To { get; set; }
}

internal class UpdateWorkingTimeCommandHandler(IEfRepository<WorkingTime> _workingTimes, IUnitOfWork _unitOfWork)
    : ICommandHandler<UpdateWorkingTimeCommand>
{
    public async Task<Result> Handle(UpdateWorkingTimeCommand command, CancellationToken cancellationToken)
    {
        var workingTime = await _workingTimes.GetByIdAsync(command.WorkingTimeId);

        if (workingTime is null)
        {
            return Error.NotFound;
        }

        UpdateWorkingTimeForDayOfWeek(workingTime, command.DayOfWeek, command.From, command.To);
        _workingTimes.Update(workingTime);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    private void UpdateWorkingTimeForDayOfWeek(WorkingTime workingTime, DayOfWeek dayOfWeek, TimeOnly newStartTime, TimeOnly newEndTime)
    {
        switch (dayOfWeek)
        {
            case DayOfWeek.Monday:
                workingTime.MondayFrom = newStartTime;
                workingTime.MondayTo = newEndTime;
                break;
            case DayOfWeek.Tuesday:
                workingTime.TuesdayFrom = newStartTime;
                workingTime.TuesdayTo = newEndTime;
                break;
            case DayOfWeek.Wednesday:
                workingTime.WednesdayFrom = newStartTime;
                workingTime.WednesdayTo = newEndTime;
                break;
            case DayOfWeek.Thursday:
                workingTime.ThursdayFrom = newStartTime;
                workingTime.ThursdayTo = newEndTime;
                break;
            case DayOfWeek.Friday:
                workingTime.FridayFrom = newStartTime;
                workingTime.FridayTo = newEndTime;
                break;
            case DayOfWeek.Saturday:
                workingTime.SaturdayFrom = newStartTime;
                workingTime.SaturdayTo = newEndTime;
                break;
            case DayOfWeek.Sunday:
                workingTime.SundayFrom = newStartTime;
                workingTime.SundayTo = newEndTime;
                break;
        }
    }
}