using Microsoft.AspNetCore.Identity;
using NSubstitute;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.ResultObject;
using Xunit;

namespace SmartSalon.Tests.Unit.Users;

public class ChangePasswordTests : TestsClass
{
    private readonly UsersManager _usersManager;
    private readonly IEmailsManager _emailsManager;
    private readonly ChangePasswordCommandHandler _handler;

    public ChangePasswordTests()
    {
        _usersManager = CreateUsersManager();
        _emailsManager = Substitute.For<IEmailsManager>();

        _handler = new ChangePasswordCommandHandler(_usersManager, _emailsManager);
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var command = new ChangePasswordCommand
        {
            UserId = Id.NewGuid(),
            CurrentPassword = "currentPassword",
            NewPassword = "newPassword"
        };

        _usersManager.FindByIdAsync(command.UserId.ToString())!.Returns(Task.FromResult<User?>(null));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundError>(result.Errors!.First());
    }

    [Fact]
    public async Task ShouldReturnError_WhenChangePasswordFails()
    {
        // Arrange
        var command = new ChangePasswordCommand
        {
            UserId = Id.NewGuid(),
            CurrentPassword = "currentPassword",
            NewPassword = "newPassword"
        };

        var user = CreateUserWithId(command.UserId);
        var identityError = new IdentityError { Description = "Password change failed" };
        var identityResult = IdentityResult.Failed(identityError);

        _usersManager
            .FindByIdAsync(command.UserId.ToString())
            !.Returns(Task.FromResult(user));

        _usersManager
            .ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword)
            .Returns(Task.FromResult(identityResult));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(identityResult.Errors.First().Description, result.Errors!.First().Description);
    }

    [Fact]
    public async Task ShouldChangePasswordSuccessfully_AndSendEmail()
    {
        // Arrange
        var command = new ChangePasswordCommand
        {
            UserId = Id.NewGuid(),
            CurrentPassword = "currentPassword",
            NewPassword = "newPassword"
        };

        var user = CreateUserWithId(command.UserId);
        var identityResult = IdentityResult.Success;

        _usersManager
            .FindByIdAsync(command.UserId.ToString())
            !.Returns(Task.FromResult(user));

        _usersManager
            .ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword)
            .Returns(Task.FromResult(identityResult));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        await _emailsManager
            .Received()
            .SendRestorePasswordEmailAsync(
                user.Email,
                Arg.Any<RestorePasswordEncryptionModel>(),
                Arg.Any<RestorePasswordViewModel>()
            );

        Assert.True(result.IsSuccess);
    }
}