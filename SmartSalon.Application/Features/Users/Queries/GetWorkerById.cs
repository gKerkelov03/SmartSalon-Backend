using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class GetWorkerByIdQuery : IQuery<GetWorkerByIdQueryResponse>
{
    public Id WorkerId { get; set; }

    public GetWorkerByIdQuery(Id workerId) => WorkerId = workerId;
}

public class GetWorkerByIdQueryResponse : IMapFrom<Worker>
{
    public required string PhoneNumber { get; set; }
    public required string JobTitle { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Nickname { get; set; }
    public required string FirstName { get; set; }
}

internal class GetWorkerByIdQueryHandler(IEfRepository<Worker> _workers, IMapper _mapper)
    : IQueryHandler<GetWorkerByIdQuery, GetWorkerByIdQueryResponse>
{
    public async Task<Result<GetWorkerByIdQueryResponse>> Handle(GetWorkerByIdQuery query, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(query.WorkerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetWorkerByIdQueryResponse>(worker);
    }
}
