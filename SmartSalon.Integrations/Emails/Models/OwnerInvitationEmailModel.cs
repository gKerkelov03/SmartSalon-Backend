namespace SmartSalon.Integrations.Emails.Models;

public class OwnerInvitationEmailModel
{
    public required string WorkerName { get; set; }
    public required string OwnerName { get; set; }
    public required string SalonName { get; set; }
}