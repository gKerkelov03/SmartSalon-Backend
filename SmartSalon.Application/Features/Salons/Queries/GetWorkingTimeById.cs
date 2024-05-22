using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetWorkingTimeByIdQuery(Id id) : IQuery<GetWorkingTimeByIdQueryResponse>
{
    public Id WorkingTimeId => id;
}

public class GetWorkingTimeByIdQueryResponse : IMapFrom<WorkingTime>
{
    public Id Id { get; set; }
    public Id SalonId { get; set; }

    //TODO: maybe extract the data for each day in an owned entities of type DailyWorkingTime
    public bool MondayIsWorking { get; set; }
    public TimeOnly MondayOpeningTime { get; set; }
    public TimeOnly MondayClosingTime { get; set; }

    public bool TuesdayIsWorking { get; set; }
    public TimeOnly TuesdayOpeningTime { get; set; }
    public TimeOnly TuesdayClosingTime { get; set; }

    public bool WednesdayIsWorking { get; set; }
    public TimeOnly WednesdayOpeningTime { get; set; }
    public TimeOnly WednesdayClosingTime { get; set; }

    public bool ThursdayIsWorking { get; set; }
    public TimeOnly ThursdayOpeningTime { get; set; }
    public TimeOnly ThursdayClosingTime { get; set; }

    public bool FridayIsWorking { get; set; }
    public TimeOnly FridayOpeningTime { get; set; }
    public TimeOnly FridayClosingTime { get; set; }

    public bool SaturdayIsWorking { get; set; }
    public TimeOnly SaturdayOpeningTime { get; set; }
    public TimeOnly SaturdayClosingTime { get; set; }

    public bool SundayIsWorking { get; set; }
    public TimeOnly SundayOpeningTime { get; set; }
    public TimeOnly SundayClosingTime { get; set; }
}

internal class GetWorkingTimeByIdQueryHandler(IEfRepository<WorkingTime> _workingTimes, IMapper _mapper)
    : IQueryHandler<GetWorkingTimeByIdQuery, GetWorkingTimeByIdQueryResponse>
{
    public async Task<Result<GetWorkingTimeByIdQueryResponse>> Handle(GetWorkingTimeByIdQuery query, CancellationToken cancellationToken)
    {
        var workingTime = await _workingTimes.GetByIdAsync(query.WorkingTimeId);

        if (workingTime is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetWorkingTimeByIdQueryResponse>(workingTime);
    }
}