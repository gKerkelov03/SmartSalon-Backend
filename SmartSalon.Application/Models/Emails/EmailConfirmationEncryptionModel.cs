namespace SmartSalon.Application.Models.Emails;

public class EmailConfirmationEncryptionModel
{
    public Id UserId { get; set; }
    public required string EmailToBeConfirmed { get; set; }
}