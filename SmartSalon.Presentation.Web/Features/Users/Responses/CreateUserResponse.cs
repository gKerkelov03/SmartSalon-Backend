
using SmartSalon.Application.Abstractions.Mapping;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class CreateUserResponse : IMapFrom<CreateUserCommandResponse>
{
    public required Id CreatedUserId { get; set; }
}

public class CreateUserCommandResponse
{
}