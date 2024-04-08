using FluentEmail.Core;
using SmartSalon.Application.Abstractions;

namespace SmartSalon.Integrations.Emails;

public class EmailsManager(IFluentEmail _emailSender) : IEmailsManager
{
    private static string _templatesFolder = Path.Combine(AppDirectory, "SmartSalon.Integrations", "Emails", "Templates");

    public async Task SendEmailConfirmationEmailAsync(string recipientEmail, object model)
    {
        var emailConfirmationEmailTemplateName = "confirm-your-email.html";
        var template = File.ReadAllText(Path.Combine(_templatesFolder, emailConfirmationEmailTemplateName));
        var subject = "Confirm your email";

        await SendEmailAsync(recipientEmail, subject, template, model);
    }

    public async Task SendOwnerInvitationEmailAsync(string recipientEmail, object model)
    {
        var emailConfirmationEmailTemplateName = "invite-owner.html";
        var template = File.ReadAllText(Path.Combine(_templatesFolder, emailConfirmationEmailTemplateName));
        var subject = "Join a salon invitation";

        await SendEmailAsync(recipientEmail, subject, template, model);
    }

    public async Task SendWorkerInvitationEmailAsync(string recipientEmail, object model)
    {
        var emailConfirmationEmailTemplateName = "invite-worker.html";
        var template = File.ReadAllText(Path.Combine(_templatesFolder, emailConfirmationEmailTemplateName));
        var subject = "Join a salon invitation";

        await SendEmailAsync(recipientEmail, subject, template, model);
    }

    private async Task SendEmailAsync(string recipientEmail, string subject, string template, object model)
        => await _emailSender
            .To(recipientEmail)
            .Subject(subject)
            .UsingTemplate(template, model)
            .SendAsync();
}