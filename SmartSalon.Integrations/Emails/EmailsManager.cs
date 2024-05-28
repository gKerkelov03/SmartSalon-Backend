using FluentEmail.Core;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.Options;

namespace SmartSalon.Integrations.Emails;

public class EmailsManager(
    IFluentEmail _emailSender,
    IEncryptor _encryptor,
    IOptions<EmailOptions> _emailOptions,
    IOptions<HostingOptions> _hostingOptions
) : IEmailsManager
{
    private static string _templatesFolder = Path.Combine(AppDirectory, "SmartSalon.Integrations", "Emails", "Templates");

    public async Task SendEmailConfirmationEmailAsync(
        string recipientEmail,
        EmailConfirmationEncryptionModel encryptionModel,
        EmailConfirmationViewModel viewModel
    )
    {
        var templateName = "confirm-email.html";
        var template = File.ReadAllText(Path.Combine(_templatesFolder, templateName));
        var subject = "Confirm your email";

        var token = _encryptor.Encrypt(encryptionModel, _emailOptions.Value.EncryptionKey);

        var viewModelWithFrontendUrl = new
        {
            viewModel.UserFirstName,
            FrontendUrl = GetFrontendUrl(token, EmailType.EmailConfirmation)
        };

        await SendEmailAsync(recipientEmail, subject, template, viewModelWithFrontendUrl);
    }

    public async Task SendOwnerInvitationEmailAsync(
        string recipientEmail,
        OwnerInvitationEncryptionModel encryptionModel,
        OwnerInvitationViewModel viewModel
    )
    {
        var templateName = "invite-owner.html";
        var template = File.ReadAllText(Path.Combine(_templatesFolder, templateName));
        var subject = "Join a salon invitation";
        var token = _encryptor.Encrypt(encryptionModel, _emailOptions.Value.EncryptionKey);

        var viewModelWithFrontendUrl = new
        {
            viewModel.OwnerFirstName,
            viewModel.SalonName,
            FrontendUrl = GetFrontendUrl(token, EmailType.OwnerInvitation),
        };

        await SendEmailAsync(recipientEmail, subject, template, viewModelWithFrontendUrl);
    }

    public async Task SendWorkerInvitationEmailAsync(
        string recipientEmail,
        WorkerInvitationEncryptionModel encryptionModel,
        WorkerInvitationViewModel viewModel
    )
    {
        var templateName = "invite-worker.html";
        var template = File.ReadAllText(Path.Combine(_templatesFolder, templateName));
        var subject = "Join a salon invitation";
        var token = _encryptor.Encrypt(encryptionModel, _emailOptions.Value.EncryptionKey);

        var viewModelWithFrontendUrl = new
        {
            viewModel.WorkerFirstName,
            viewModel.SalonName,
            FrontendUrl = GetFrontendUrl(token, EmailType.WorkerInvitation),
        };

        await SendEmailAsync(recipientEmail, subject, template, viewModelWithFrontendUrl);
    }

    public async Task SendRestorePasswordEmailAsync(
        string recipientEmail,
        RestorePasswordEncryptionModel encryptionModel,
        RestorePasswordViewModel viewModel
    )
    {
        var templateName = "restore-password.html";
        var template = File.ReadAllText(Path.Combine(_templatesFolder, templateName));
        var subject = "Your password was changed";
        var token = _encryptor.Encrypt(encryptionModel, _emailOptions.Value.EncryptionKey);

        var viewModelWithFrontendUrl = new
        {
            viewModel.UserFirstName,
            Token = token,
            FrontendUrl = GetFrontendUrl(token, EmailType.RestorePassword),
        };

        await SendEmailAsync(recipientEmail, subject, template, viewModelWithFrontendUrl);
    }

    public async Task SendBookingCancellationEmailAsync(string recipientEmail, BookingCancellationViewModel viewModel)
    {
        var templateName = "cancel-booking.html";
        var template = File.ReadAllText(Path.Combine(_templatesFolder, templateName));
        var subject = "Your booking was cancelled";

        await SendEmailAsync(recipientEmail, subject, template, viewModel);
    }

    private string GetFrontendUrl(string token, EmailType emailType)
        => $"{_hostingOptions.Value.FrontendUrl}/public/emails-handler?token={token}&email-type={emailType.CastTo<int>()}";

    private async Task SendEmailAsync(string recipientEmail, string subject, string template, object viewModel)
        => await _emailSender
            .To(recipientEmail)
            .Subject(subject)
            .UsingTemplate(template, viewModel)
            .SendAsync();

}