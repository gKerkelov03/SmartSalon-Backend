using AutoMapper;
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
        var ownersMatchingTheSearchTerm = await _owners.FindAllAsync(owner =>
            owner.Email!.Contains(query.SearchTerm) ||
            owner.PhoneNumber!.Contains(query.SearchTerm) ||
            (owner.FirstName + " " + owner.LastName).Contains(query.SearchTerm)
        );

        if (ownersMatchingTheSearchTerm.IsEmpty())
        {
            return Error.NotFound;
        }

        return ownersMatchingTheSearchTerm.ToListOf<GetOwnerByIdQueryResponse>(_mapper);
    }
}
