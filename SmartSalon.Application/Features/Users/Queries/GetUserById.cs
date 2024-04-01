using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.MediatR;
using SmartSalon.Application.ResultObject;

namespace SmartSalon.Application.Features.Users.Queries;

public class GetUserByIdQuery : ICachedQuery<GetUserByIdQueryResponse>
{
    public Id UserId { get; set; }

    public string CachingKey => UserId.ToString();
}

public class GetUserByIdQueryResponse
{
    public required string UserName { get; set; }

    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string PictureUrl { get; set; }
}

internal class GetUserByIdQueryHandler(IUnitOfWork _unitOfWork, UsersManager _usersManager)
    : IQueryHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
{
    public async Task<Result<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        var response = new GetUserByIdQueryResponse()
        {
            UserName = "Shabi",
            FirstName = "Shalabi",
            LastName = "Shabililibi",
            Email = "shabi@abv.bg",
            PictureUrl = "somepictureurl"
        };

        return await Task.FromResult(response);
    }
}
