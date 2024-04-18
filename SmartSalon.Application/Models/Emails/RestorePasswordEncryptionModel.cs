namespace SmartSalon.Application.Models.Emails;

public class RestorePasswordEncryptionModel
{
    public Id UserId { get; set; }
    public DateOnly ExpirationDate { get; set; }
}