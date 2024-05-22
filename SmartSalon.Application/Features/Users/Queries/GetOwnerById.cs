using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class GetOwnerByIdQuery(Id ownerId) : IQuery<GetOwnerByIdQueryResponse>
{
    public Id OwnerId => ownerId;
}

public class GetOwnerByIdQueryResponse : IMapFrom<Owner>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public required string PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required IEnumerable<Id> SalonsOwned { get; set; }
}

internal class GetOwnerByIdQueryHandler(IEfRepository<Owner> _owners)
    : IQueryHandler<GetOwnerByIdQuery, GetOwnerByIdQueryResponse>
{
    public async Task<Result<GetOwnerByIdQueryResponse>> Handle(GetOwnerByIdQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _owners.All
            .Where(owner => owner.Id == query.OwnerId)
            .Include(owner => owner.Salons)
            .Select(owner => new GetOwnerByIdQueryResponse
            {
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                Email = owner.Email,
                PhoneNumber = owner.PhoneNumber,
                ProfilePictureUrl = owner.ProfilePictureUrl,
                SalonsOwned = owner.Salons!.Select(salon => salon.Id)
            })
            .FirstOrDefaultAsync();

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
