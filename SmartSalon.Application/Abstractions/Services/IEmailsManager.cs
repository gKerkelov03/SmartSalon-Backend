
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Application.Abstractions;

public interface IEmailsManager : ISingletonLifetime
{
    Task SendConfirmationEmailAsync(string recipientEmail, object model);
}