using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class SearchForUnemployedWorkerQuery : IQuery<IEnumerable<GetOwnerByIdQueryResponse>>
{
    public string searchTerm { get; set; }

    public SearchForUnemployedWorkerQuery(string searchTerm) => this.searchTerm = searchTerm;
}

internal class SearchForUnemployedWorkerQueryHandler(IEfRepository<Owner> _workers, IMapper _mapper)
    : IQueryHandler<SearchForUnemployedWorkerQuery, IEnumerable<GetOwnerByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetOwnerByIdQueryResponse>>> Handle(SearchForUnemployedWorkerQuery query, CancellationToken cancellationToken)
    {
        var workers = await _workers.FindAllAsync(worker =>
            worker.Email!.Contains(query.searchTerm) ||
            worker.PhoneNumber!.Contains(query.searchTerm) ||
            (worker.FirstName + " " + worker.LastName).Contains(query.searchTerm)
        );

        if (workers.IsEmpty())
        {
            return Error.NotFound;
        }

        return workers.ToListOf<GetOwnerByIdQueryResponse>(_mapper);
    }
}
