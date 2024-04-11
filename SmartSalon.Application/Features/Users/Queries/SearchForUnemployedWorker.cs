using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class SearchForUnemployedWorkerQuery(string searchTerm) : IQuery<IEnumerable<GetOwnerByIdQueryResponse>>
{
    public string SearchTerm => searchTerm;
}

internal class SearchForUnemployedWorkerQueryHandler(IEfRepository<Owner> _workers, IMapper _mapper)
    : IQueryHandler<SearchForUnemployedWorkerQuery, IEnumerable<GetOwnerByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetOwnerByIdQueryResponse>>> Handle(SearchForUnemployedWorkerQuery query, CancellationToken cancellationToken)
    {
        var workers = await _workers.FindAllAsync(worker =>
            worker.Email!.Contains(query.SearchTerm) ||
            worker.PhoneNumber!.Contains(query.SearchTerm) ||
            (worker.FirstName + " " + worker.LastName).Contains(query.SearchTerm)
        );

        if (workers.IsEmpty())
        {
            return Error.NotFound;
        }

        return workers.ToListOf<GetOwnerByIdQueryResponse>(_mapper);
    }
}
