using FluentEmail.Core;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.Options;

namespace SmartSalon.Integrations.Emails;

public class EmailsManager(
    IFluentEmail _emailSender,
    IEncryptionHelper _encryptionHelper,
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
        var templateName = "confirm-your-email.html";
        var template = File.ReadAllText(Path.Combine(_templatesFolder, templateName));
        var subject = "Confirm your email";
        var backendEndpointUrl = $"{_hostingOptions.Value.BackendUrl}/Api/V1/Users/ConfirmEmail";
        var frontendRedirectUrl = $"{_hostingOptions.Value.FrontendUrl}/main/profile";

        var token = _encryptionHelper.Encrypt(encryptionModel, _emailOptions.Value.EncryptionKey);
        var extendedViewModel = new
        {
            viewModel.UserFirstName,
            Token = token,
            BackendEndpointUrl = backendEndpointUrl,
            FrontendRedirectUrl = frontendRedirectUrl,
        };

        await SendEmailAsync(recipientEmail, subject, template, extendedViewModel);
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
        var backendEndpointUrl = $"{_hostingOptions.Value.BackendUrl}/Api/V1/Owners/AddToSalon";
        var frontendRedirectUrl = $"{_hostingOptions.Value.FrontendUrl}/main/my-salons";

        var token = _encryptionHelper.Encrypt(encryptionModel, _emailOptions.Value.EncryptionKey);
        var extendedViewModel = new
        {
            viewModel.OwnerFirstName,
            viewModel.SalonName,
            Token = token,
            BackendEndpointUrl = backendEndpointUrl,
            FrontendRedirectUrl = frontendRedirectUrl,
        };

        await SendEmailAsync(recipientEmail, subject, template, extendedViewModel);
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
        var backendEndpointUrl = $"{_hostingOptions.Value.BackendUrl}/Api/V1/Owners/AddToSalon";
        var frontendRedirectUrl = $"{_hostingOptions.Value.FrontendUrl}/main/my-salons";

        var token = _encryptionHelper.Encrypt(encryptionModel, _emailOptions.Value.EncryptionKey);
        var extendedViewModel = new
        {
            viewModel.WorkerFirstName,
            viewModel.SalonName,
            Token = token,
            BackendEndpointUrl = backendEndpointUrl,
            FrontendRedirectUrl = frontendRedirectUrl,
        };

        await SendEmailAsync(recipientEmail, subject, template, extendedViewModel);
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
        var frontendRedirectUrl = $"{_hostingOptions.Value.FrontendUrl}/public/reset-password";

        var token = _encryptionHelper.Encrypt(encryptionModel, _emailOptions.Value.EncryptionKey);
        var extendedViewModel = new
        {
            viewModel.UserFirstName,
            Token = token,
            FrontendRedirectUrl = frontendRedirectUrl,
        };

        await SendEmailAsync(recipientEmail, subject, template, extendedViewModel);
    }

    private async Task SendEmailAsync(string recipientEmail, string subject, string template, object viewModel)
        => await _emailSender
            .To(recipientEmail)
            .Subject(subject)
            .UsingTemplate(template, viewModel)
            .SendAsync();
}