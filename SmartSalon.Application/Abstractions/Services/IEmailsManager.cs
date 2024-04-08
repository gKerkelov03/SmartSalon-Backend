
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Application.Abstractions;

public interface IEmailsManager : ISingletonLifetime
{
    Task SendEmailConfirmationEmailAsync(string recipientEmail, object model);
    Task SendWorkerInvitationEmailAsync(string recipientEmail, object model);
    Task SendOwnerInvitationEmailAsync(string recipientEmail, object model);
}