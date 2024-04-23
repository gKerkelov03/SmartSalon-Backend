namespace SmartSalon.Presentation.Web.Features.Salons.Controllers;

public class AddSpecialtyRequest
{
    public required string Text { get; set; }
    public Id SalonId { get; set; }
}