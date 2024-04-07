using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class SearchForOwnerQuery : IQuery<IEnumerable<GetOwnerByIdQueryResponse>>
{
    public string searchTerm { get; set; }

    public SearchForOwnerQuery(string searchTerm) => this.searchTerm = searchTerm;
}

internal class SearchForOwnerQueryHandler(IEfRepository<Owner> _owners)
    : IQueryHandler<SearchForOwnerQuery, IEnumerable<GetOwnerByIdQueryResponse>>
{
    public async Task<Result<IEnumerable<GetOwnerByIdQueryResponse>>> Handle(SearchForOwnerQuery query, CancellationToken cancellationToken)
    {
        var ownersMatchingTheSearchTerm = await _owners.FindAllAsync(owner =>
            owner.Email!.Contains(query.searchTerm) ||
            owner.PhoneNumber!.Contains(query.searchTerm) ||
            (owner.FirstName + " " + owner.LastName).Contains(query.searchTerm)
        );

        if (ownersMatchingTheSearchTerm.IsEmpty())
        {
            return Error.NotFound;
        }

        return ownersMatchingTheSearchTerm.ToListOf<GetOwnerByIdQueryResponse>();
    }
}
