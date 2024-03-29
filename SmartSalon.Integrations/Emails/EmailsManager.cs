using FluentEmail.Core;
using SmartSalon.Application.Abstractions;

namespace SmartSalon.Integrations.Emails;

public class EmailsManager(IFluentEmail _emailSender) : IEmailsManager
{
    public async Task SendConfirmationEmailAsync(string recipientEmail, object model)
    {
        var templatePath = Path.Combine(AppDirectory, "SmartSalon.Integrations", "Emails", "Templates");
        var template = File.ReadAllText(templatePath);

        await _emailSender
            .To(recipientEmail)
            .Subject("Confirm your email")
            .UsingTemplate(template, model)
            .SendAsync();
    }
}