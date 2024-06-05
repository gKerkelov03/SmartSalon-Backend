using Microsoft.Extensions.Options;
using NSubstitute;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Abstractions.Services;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Application.Models.Emails;
using SmartSalon.Application.Options;
using Xunit;

namespace SmartSalon.Tests.Unit.Users;

public class AddOwnerToSalonTests : TestsClass
{
    private readonly IEfRepository<Owner> _owners;
    private readonly IEfRepository<Salon> _salons;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOptions<EmailOptions> _emailOptions;
    private readonly IDecryptor _decryptor;
    private readonly AddOwnerToSalonCommandHandler _handler;

    public AddOwnerToSalonTests()
    {

        _owners = Substitute.For<IEfRepository<Owner>>();
        _salons = Substitute.For<IEfRepository<Salon>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _emailOptions = Substitute.For<IOptions<EmailOptions>>();
        _decryptor = Substitute.For<IDecryptor>();

        _emailOptions.Value.Returns(CreateEmailOptions());

        _handler = new AddOwnerToSalonCommandHandler(
            _owners,
            _salons,
            _unitOfWork,
            _emailOptions,
            _decryptor
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDecryptedTokenIsNull()
    {
        // Arrange
        var command = new AddOwnerToSalonCommand(Arg.Any<string>());
        _decryptor.DecryptTo<OwnerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns((OwnerInvitationEncryptionModel?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors!);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenOwnerOrSalonDoesNotExist()
    {
        // Arrange
        var command = new AddOwnerToSalonCommand(Arg.Any<string>());
        var ownerId = Guid.NewGuid();
        var salonId = Guid.NewGuid();

        var decryptedToken = new OwnerInvitationEncryptionModel
        {
            OwnerId = ownerId,
            SalonId = salonId
        };

        _decryptor
            .DecryptTo<OwnerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns(decryptedToken);

        _owners.GetByIdAsync(decryptedToken.OwnerId).Returns((Owner?)null);
        _salons.All.Returns(new List<Salon>().AsQueryable());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors!, e => e == Error.NotFound);
    }

    [Fact]
    public async Task Handle_ShouldAddOwnerToSalon_WhenOwnerAndSalonExist()
    {
        // Arrange
        var command = new AddOwnerToSalonCommand("valid-token");
        var ownerId = Guid.NewGuid();
        var salonId = Guid.NewGuid();
        var decryptedToken = new OwnerInvitationEncryptionModel { OwnerId = ownerId, SalonId = salonId };
        var owner = CreateOwnerWithId(ownerId);
        var salon = CreateSalonWithId(salonId);

        _decryptor
            .DecryptTo<OwnerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns(decryptedToken);

        _owners.GetByIdAsync(decryptedToken.OwnerId).Returns(owner);
        _salons.All.Returns(new List<Salon> { salon }.AsQueryable());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Contains(owner, salon.Owners!);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }
}