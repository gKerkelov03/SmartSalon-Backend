
using SmartSalon.Application.Abstractions.Lifetimes;

namespace SmartSalon.Application.Abstractions;

public interface IEmailsManager : ISingletonLifetime
{
    Task SendConfirmationEmailAsync(string recipientEmail, object model);
}