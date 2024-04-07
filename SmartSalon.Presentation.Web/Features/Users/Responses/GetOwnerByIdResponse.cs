namespace SmartSalon.Presentation.Web.Features.Users.Responses;

public class GetOwnerByIdResponse
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PictureUrl { get; set; }
    public required string PhoneNumber { get; set; }
}