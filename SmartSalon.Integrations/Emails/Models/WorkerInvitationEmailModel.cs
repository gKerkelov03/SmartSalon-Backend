namespace SmartSalon.Integrations.Emails.Models;

public class WorkerInvitationEmailModel
{
    public required string InvitedOwnerName { get; set; }
    public required string OwnerSendedTheInviteName { get; set; }
    public required string SalonName { get; set; }
}