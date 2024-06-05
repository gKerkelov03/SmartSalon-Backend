global using UsersManager = Microsoft.AspNetCore.Identity.UserManager<SmartSalon.Application.Domain.Users.User>;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NSubstitute;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.Options;
using Xunit;

namespace SmartSalon.Tests.Unit.Users;

public class ChangeEmailTests : TestsClass
{
    private readonly UsersManager _usersManager;
    private readonly IDecryptor _decryptor;
    private readonly IOptions<EmailOptions> _emailOptions;
    private readonly ChangeEmailCommandHandler _handler;

    public ChangeEmailTests()
    {
        _usersManager = CreateUsersManager();
        _decryptor = Substitute.For<IDecryptor>();
        _emailOptions = Substitute.For<IOptions<EmailOptions>>();

        _emailOptions.Value.Returns(CreateEmailOptions());

        _handler = new ChangeEmailCommandHandler(_usersManager, _decryptor, _emailOptions);
    }

    [Fact]
    public async Task ShouldReturnError_WhenDecryptedTokenIsNull()
    {
        // Arrange
        var command = new ChangeEmailCommand(Arg.Any<string>());

        _decryptor
            .DecryptTo<EmailConfirmationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns((EmailConfirmationEncryptionModel?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors!);
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var newEmail = "newemail@example.com";
        var command = new ChangeEmailCommand(Arg.Any<string>());
        var decryptedToken = new EmailConfirmationEncryptionModel
        {
            UserId = Guid.NewGuid(),
            EmailToBeConfirmed = newEmail
        };

        _decryptor
            .DecryptTo<EmailConfirmationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns(decryptedToken);

        _usersManager.FindByIdAsync(decryptedToken.UserId.ToString()).Returns((User?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task ShouldReturnError_WhenChangeEmailFails()
    {
        // Arrange
        var newEmail = "newemail@example.com";
        var changeEmailToken = new Guid().ToString();
        var command = new ChangeEmailCommand(Arg.Any<string>());
        var decryptedToken = new EmailConfirmationEncryptionModel
        {
            UserId = Guid.NewGuid(),
            EmailToBeConfirmed = newEmail
        };
        var user = CreateUserWithId(decryptedToken.UserId);

        _decryptor.DecryptTo<EmailConfirmationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns(decryptedToken);

        _usersManager.FindByIdAsync(decryptedToken.UserId.ToString()).Returns(user);
        _usersManager.ChangeEmailAsync(user, decryptedToken.EmailToBeConfirmed, changeEmailToken)
            .Returns(IdentityResult.Failed(new IdentityError { Description = "Change email failed" }));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
    }

    [Fact]
    public async Task ShouldChangeEmailSuccessfully_WhenUserAndTokenAreValid()
    {
        // Arrange
        var newEmail = "newemail@example.com";
        var changeEmailToken = new Guid().ToString();
        var command = new ChangeEmailCommand(Arg.Any<string>());
        var decryptedToken = new EmailConfirmationEncryptionModel
        {
            UserId = Guid.NewGuid(),
            EmailToBeConfirmed = newEmail
        };

        var user = CreateUserWithId(decryptedToken.UserId);

        _decryptor.DecryptTo<EmailConfirmationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns(decryptedToken);

        _usersManager.FindByIdAsync(decryptedToken.UserId.ToString()).Returns(user);
        _usersManager.ChangeEmailAsync(user, decryptedToken.EmailToBeConfirmed, changeEmailToken).Returns(IdentityResult.Success);
        _usersManager.SetUserNameAsync(user, decryptedToken.EmailToBeConfirmed).Returns(IdentityResult.Success);
        _usersManager.ConfirmEmailAsync(user, changeEmailToken).Returns(IdentityResult.Success);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
    }
}