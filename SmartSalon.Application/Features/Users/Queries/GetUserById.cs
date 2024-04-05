using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

internal class GetUserByIdQueryHandler(IEfRepository<User> _users)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var user = await _users.GetByIdAsync(query.UserId);

        if (user is null)
        {
            return Error.NotFound;
        }

        return user.MapTo<GetUserByIdQueryResponse>();
    }
}

public class GetUserByIdQuery : IQuery<GetUserByIdQueryResponse>
{
    public Id UserId { get; set; }

    public GetUserByIdQuery(Id userId) => UserId = userId;
}

public class GetUserByIdQueryResponse
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string UserName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
    public required string PictureUrl { get; set; }
}