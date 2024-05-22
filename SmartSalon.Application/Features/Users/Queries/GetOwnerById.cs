using AutoMapper;
using AutoMapper.QueryableExtensions;
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

public class GetOwnerByIdQueryResponse : IHaveCustomMapping
{
    public Id Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public required string PhoneNumber { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required IEnumerable<Id> Salons { get; set; }

    public void CreateMapping(IProfileExpression config)
        => config
            .CreateMap<Owner, GetOwnerByIdQueryResponse>()
            .ForMember(
                destination => destination.Salons,
                options => options.MapFrom(source => source.Salons!.Select(salon => salon.Id))
            );
}

internal class GetOwnerByIdQueryHandler(IEfRepository<Owner> _owners, IMapper _mapper)
    : IQueryHandler<GetOwnerByIdQuery, GetOwnerByIdQueryResponse>
{
    public async Task<Result<GetOwnerByIdQueryResponse>> Handle(GetOwnerByIdQuery query, CancellationToken cancellationToken)
    {
        var queryResponse = await _owners.All
            .Include(owner => owner.Salons)
            .ProjectTo<GetOwnerByIdQueryResponse>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(owner => owner.Id == query.OwnerId);

        if (queryResponse is null)
        {
            return Error.NotFound;
        }

        return queryResponse;
    }
}
