namespace SmartSalon.Application.Models.Emails;

public class RestorePasswordEmailEncryptionModel
{
    public Id UserId { get; set; }
    public DateOnly ExpirationDate { get; set; }
}