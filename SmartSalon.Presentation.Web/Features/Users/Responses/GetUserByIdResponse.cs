
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Queries;

namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class GetUserByIdResponse : IMapFrom<GetUserByIdQueryResponse>
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
}