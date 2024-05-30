using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class GetWorkerByIdQuery(Id workerId) : IQuery<GetWorkerByIdQueryResponse>
{
    public Id WorkerId => workerId;
}

public class GetWorkerByIdQueryResponse : IHaveCustomMapping
{
    public Id Id { get; set; }
    public required string PhoneNumber { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public required string Nickname { get; set; }
    public Id? SalonId { get; set; }
    public required IEnumerable<Id> JobTitles { get; set; }
    public required IEnumerable<Id> Salons { get; set; }
    public required IEnumerable<string> Roles { get; set; }

    public void CreateMapping(IProfileExpression config)
        => config
            .CreateMap<Worker, GetWorkerByIdQueryResponse>()
            .ForMember(
                destination => destination.Salons,
                options => options.MapFrom(source => source.Salons!.Select(salon => salon.Id))
            )
            .ForMember(
                destination => destination.JobTitles,
                options => options.MapFrom(source => source.JobTitles!.Select(jobTitle => jobTitle.Id))
            );
}

internal class GetWorkerByIdQueryHandler(IEfRepository<Worker> _workers, UsersManager _users, IMapper _mapper)
    : IQueryHandler<GetWorkerByIdQuery, GetWorkerByIdQueryResponse>
{
    public async Task<Result<GetWorkerByIdQueryResponse>> Handle(GetWorkerByIdQuery query, CancellationToken cancellationToken)
    {
        var worker = await _workers.GetByIdAsync(query.WorkerId);

        if (worker is null)
        {
            return Error.NotFound;
        }

        var queryResponse = await _workers.All
            .Include(worker => worker.JobTitles)
            .Include(worker => worker.Salons)
            .ProjectTo<GetWorkerByIdQueryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(worker => worker.Id == query.WorkerId);

        queryResponse!.Roles = await _users.GetRolesAsync(worker);

        return queryResponse;
    }
}
