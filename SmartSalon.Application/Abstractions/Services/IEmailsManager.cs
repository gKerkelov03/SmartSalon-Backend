
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Models.Emails;

namespace SmartSalon.Application.Abstractions.Services;

public interface IEmailsManager : ITransientLifetime
{

    Task SendBookingCancellationEmailAsync(
        string recipientEmail,
        BookingCancellationViewModel viewModel
    );

    Task SendEmailConfirmationEmailAsync(
        string recipientEmail,
        EmailConfirmationEncryptionModel encryptionModel,
        EmailConfirmationViewModel viewModel
    );

    Task SendWorkerInvitationEmailAsync(
        string recipientEmail,
        WorkerInvitationEncryptionModel encryptionModel,
        WorkerInvitationViewModel viewModel
    );

    Task SendOwnerInvitationEmailAsync(
        string recipientEmail,
        OwnerInvitationEncryptionModel encryptionModel,
        OwnerInvitationViewModel viewModel
    );

    Task SendRestorePasswordEmailAsync(
        string recipientEmail,
        RestorePasswordEncryptionModel encryptionModel,
        RestorePasswordViewModel viewModel
    );
}
