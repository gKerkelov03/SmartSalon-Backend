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
    public required IEnumerable<string> Roles { get; set; }
}

internal class GetUserByIdQueryHandler(UsersManager _users, IMapper _mapper)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _users.FindByIdAsync(query.UserId.ToString());

        if (user is null)
        {
            return Error.NotFound;
        }

        var queryResponse = _mapper.Map<GetUserByIdQueryResponse>(user);
        queryResponse.Roles = await _users.GetRolesAsync(user);

        return queryResponse;
    }
}
