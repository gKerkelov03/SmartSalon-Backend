﻿using AutoMapper;
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

internal class SearchForUnemployedWorkerQueryHandler(IEfRepository<Worker> _workers, IMapper _mapper)
    : IQueryHandler<SearchForUnemployedWorkerQuery, IEnumerable<GetWorkerByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetWorkerByIdQueryResponse>>> Handle(
        SearchForUnemployedWorkerQuery query,
        CancellationToken cancellationToken
    )
    {
        var matchingUnemployedWorkers = await _workers.All
            .Where(worker => worker.SalonId == null && (
                worker.NormalizedEmail!.Contains(query.SearchTerm.ToUpper()) ||
                worker.PhoneNumber!.Contains(query.SearchTerm) ||
                (worker.FirstName.ToUpper() + " " + worker.LastName.ToUpper()).Contains(query.SearchTerm.ToUpper()))
            ).ToListAsync();

        if (matchingUnemployedWorkers.IsEmpty())
        {
            return Error.NotFound;
        }

        return matchingUnemployedWorkers.ToListOf<GetWorkerByIdQueryResponse>(_mapper);
    }
}
