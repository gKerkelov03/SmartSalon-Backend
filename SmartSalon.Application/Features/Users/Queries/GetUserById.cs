using AutoMapper;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class GetUserByIdQuery(Id userId) : IQuery<GetUserByIdQueryResponse>
{
    public Id UserId => userId;
}

public class GetUserByIdQueryResponse : IMapFrom<User>
{
    public required Id Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public required string ProfilePictureUrl { get; set; }
}

internal class GetUserByIdQueryHandler(IEfRepository<User> _users, IMapper _mapper)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _users.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return Error.NotFound;
        }

        return _mapper.Map<GetUserByIdQueryResponse>(user);
    }
}
