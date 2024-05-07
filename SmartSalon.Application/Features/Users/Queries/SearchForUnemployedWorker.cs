using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class SearchForUnemployedWorkerQuery(string searchTerm) : IQuery<IEnumerable<GetWorkerByIdQueryResponse>>
{
    public string SearchTerm => searchTerm;
}

internal class SearchForUnemployedWorkerQueryHandler(IEfRepository<Worker> _workers)
    : IQueryHandler<SearchForUnemployedWorkerQuery, IEnumerable<GetWorkerByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetWorkerByIdQueryResponse>>> Handle(
        SearchForUnemployedWorkerQuery query,
        CancellationToken cancellationToken
    )
    {
        var queryResponse = await _workers.All
            .Include(worker => worker.JobTitles)
            .Include(worker => worker.Salons)
            .Where(worker => worker.Salons!.IsEmpty() && (
                worker.NormalizedEmail!.Contains(query.SearchTerm.ToUpper()) ||
                worker.PhoneNumber!.Contains(query.SearchTerm) ||
                (worker.FirstName.ToUpper() + " " + worker.LastName.ToUpper()).Contains(query.SearchTerm.ToUpper()))
            )
            .Select(worker => new GetWorkerByIdQueryResponse
            {
                //TODO: we repeat this code tiwce (here and in the GetWorkerById) can we reuse it somehow
                Id = worker.Id,
                PhoneNumber = worker.PhoneNumber,
                ProfilePictureUrl = worker.ProfilePictureUrl,
                LastName = worker.LastName,
                Email = worker.Email,
                EmailConfirmed = worker.EmailConfirmed,
                Nickname = worker.Nickname,
                FirstName = worker.FirstName,
                Salons = worker.Salons!.Select(salon => salon.Id),
                JobTitles = worker.JobTitles!.Select(jobTitle => jobTitle.Id)
            }).ToListAsync();

        if (queryResponse.IsEmpty())
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
