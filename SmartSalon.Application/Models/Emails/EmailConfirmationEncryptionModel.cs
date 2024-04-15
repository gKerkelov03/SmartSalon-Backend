namespace SmartSalon.Application.Models.Emails;

public class EmailConfirmationEncryptionModel
{
    public Id UserId { get; set; }
    public required string NewEmail { get; set; }
    public required string Password { get; set; }
}