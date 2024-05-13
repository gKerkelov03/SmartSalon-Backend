using Microsoft.AspNetCore.Mvc;
using SmartSalon.Application.Abstractions.Mapping;
using SmartSalon.Application.Features.Users.Commands;

namespace SmartSalon.Presentation.Web.Features.Users.Requests;

public class UpdateUserRequest : IMapTo<UpdateUserCommand>
{
    [FromRoute(Name = IdRouteParameterName)]
    public Id UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
}