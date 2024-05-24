namespace SmartSalon.Application.Models.Emails;

public class BookingCancellationViewModel
{
    public required string UserFirstName { get; set; }
    public required string ServiceName { get; set; }
    public required string SalonName { get; set; }
}