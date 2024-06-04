using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class SearchForOwnerQuery(string searchTerm) : IQuery<IEnumerable<GetOwnerByIdQueryResponse>>
{
    public string SearchTerm => searchTerm;
}

internal class SearchForOwnerQueryHandler(IEfRepository<Owner> _owners, IMapper _mapper)
    : IQueryHandler<SearchForOwnerQuery, IEnumerable<GetOwnerByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetOwnerByIdQueryResponse>>> Handle(SearchForOwnerQuery query, CancellationToken cancellationToken)
    {
        var ownersMatchingTheSearchTerm = await _owners.All
            .Include(owner => owner.Salons)
            .Where(owner =>
                owner.NormalizedEmail!.Contains(query.SearchTerm.ToUpper()) ||
                owner.PhoneNumber!.Contains(query.SearchTerm) ||
                (owner.FirstName.ToUpper() + " " + owner.LastName.ToUpper()).Contains(query.SearchTerm.ToUpper())
            )
            .ToListAsync();

        if (ownersMatchingTheSearchTerm.IsEmpty())
        {
            return Error.NotFound;
        }

        return ownersMatchingTheSearchTerm.ToListOf<GetOwnerByIdQueryResponse>(_mapper);
    }
}
