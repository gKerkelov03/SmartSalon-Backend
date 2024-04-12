
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Models.Emails;

namespace SmartSalon.Application.Abstractions.Services;

public interface IEmailsManager : ISingletonLifetime
{
    Task SendEmailConfirmationEmailAsync(
        string recipientEmail,
        EmailConfirmationEmailEncryptionModel encryptionModel,
        EmailConfirmationEmailViewModel viewModel
    );

    Task SendWorkerInvitationEmailAsync(
        string recipientEmail,
        WorkerInvitationEmailEncryptionModel encryptionModel,
        WorkerInvitationEmailViewModel viewModel
    );

    Task SendOwnerInvitationEmailAsync(
        string recipientEmail,
        OwnerInvitationEmailEncryptionModel encryptionModel,
        OwnerInvitationEmailViewModel viewModel
    );

    Task SendRestorePasswordEmailAsync(
        string recipientEmail,
        RestorePasswordEmailEncryptionModel encryptionModel,
        RestorePasswordEmailViewModel viewModel
    );
}
