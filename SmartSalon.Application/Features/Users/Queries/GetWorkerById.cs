using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class GetWorkerByIdQuery(Id workerId) : IQuery<GetWorkerByIdQueryResponse>
{
    public Id WorkerId => workerId;
}

public class GetWorkerByIdQueryResponse : IMapFrom<Worker>
{
    public Id Id { get; set; }
    public required string PhoneNumber { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public required string Nickname { get; set; }
    public required string FirstName { get; set; }
    public Id? SalonId { get; set; }
    public required IEnumerable<Id> JobTitles { get; set; }
}

internal class GetWorkerByIdQueryHandler(IEfRepository<Worker> _workers)
    : IQueryHandler<GetWorkerByIdQuery, GetWorkerByIdQueryResponse>
{
    public async Task<Result<GetWorkerByIdQueryResponse>> Handle(GetWorkerByIdQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _workers.All
            .Include(worker => worker.JobTitles)
            .Where(worker => worker.Id == query.WorkerId)
            .Select(worker => new GetWorkerByIdQueryResponse
            {
                Id = worker.Id,
                PhoneNumber = worker.PhoneNumber,
                ProfilePictureUrl = worker.ProfilePictureUrl,
                LastName = worker.LastName,
                Email = worker.Email,
                EmailConfirmed = worker.EmailConfirmed,
                Nickname = worker.Nickname,
                FirstName = worker.FirstName,
                SalonId = worker.SalonId,
                JobTitles = worker.JobTitles!.Select(jobTitle => jobTitle.Id)
            })
            .FirstOrDefaultAsync();

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
