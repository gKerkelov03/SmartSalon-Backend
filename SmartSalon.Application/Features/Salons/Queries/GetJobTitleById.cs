using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Salons.Queries;

public class GetJobTitleByIdQuery(Id id) : IQuery<GetJobTitleByIdQueryResponse>
{
    public Id JobTitleId => id;
}

public class GetJobTitleByIdQueryResponse : IMapFrom<JobTitle>
{
    public required string Name { get; set; }
    public Id Id { get; set; }
}

internal class GetJobTitleByIdQueryHandler(IEfRepository<JobTitle> _jobTitles, IMapper _mapper)
    : IQueryHandler<GetJobTitleByIdQuery, GetJobTitleByIdQueryResponse>
{
    public async Task<Result<GetJobTitleByIdQueryResponse>> Handle(GetJobTitleByIdQuery query, CancellationToken cancellationToken)
    {
        var jobTitle = await _jobTitles.GetByIdAsync(query.JobTitleId);

        if (jobTitle is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetJobTitleByIdQueryResponse>(jobTitle);
    }
}
