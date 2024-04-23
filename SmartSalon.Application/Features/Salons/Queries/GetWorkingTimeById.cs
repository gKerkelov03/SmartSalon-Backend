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
    public required string Text { get; set; }
    public Id SalonId { get; set; }
    public virtual Salon? Salon { get; set; }
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