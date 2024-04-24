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

    public TimeOnly MondayFrom { get; set; }
    public TimeOnly MondayTo { get; set; }

    public TimeOnly TuesdayFrom { get; set; }
    public TimeOnly TuesdayTo { get; set; }

    public TimeOnly WednesdayFrom { get; set; }
    public TimeOnly WednesdayTo { get; set; }

    public TimeOnly ThursdayFrom { get; set; }
    public TimeOnly ThursdayTo { get; set; }

    public TimeOnly FridayFrom { get; set; }
    public TimeOnly FridayTo { get; set; }

    public TimeOnly SaturdayFrom { get; set; }
    public TimeOnly SaturdayTo { get; set; }

    public TimeOnly SundayFrom { get; set; }
    public TimeOnly SundayTo { get; set; }
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