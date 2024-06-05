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

public class AddWorkerToSalonTests : TestsClass
{
    private readonly IEfRepository<Worker> _workersRepository;
    private readonly IEfRepository<Salon> _salonsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOptions<EmailOptions> _emailOptions;
    private readonly IDecryptor _decryptor;
    private readonly AddWorkerToSalonCommandHandler _handler;

    public AddWorkerToSalonTests()
    {
        _workersRepository = Substitute.For<IEfRepository<Worker>>();
        _salonsRepository = Substitute.For<IEfRepository<Salon>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _emailOptions = Substitute.For<IOptions<EmailOptions>>();
        _decryptor = Substitute.For<IDecryptor>();
        _handler = new AddWorkerToSalonCommandHandler(
            _workersRepository,
            _salonsRepository,
            _unitOfWork,
            _emailOptions,
            _decryptor
        );

        _emailOptions.Value.Returns(CreateEmailOptions());
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenDecryptedTokenIsNull()
    {
        // Arrange
        var command = new AddWorkerToSalonCommand(Arg.Any<string>());

        _decryptor
            .DecryptTo<WorkerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns((WorkerInvitationEncryptionModel?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Invalid token", result.Errors!.First().Description);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenWorkerIsNotFound()
    {
        // Arrange
        var command = new AddWorkerToSalonCommand(Arg.Any<string>());
        var decryptedToken = new WorkerInvitationEncryptionModel
        {
            WorkerId = Id.NewGuid(),
            SalonId = Id.NewGuid()
        };

        _decryptor
            .DecryptTo<WorkerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns(decryptedToken);

        _workersRepository.GetByIdAsync(decryptedToken.WorkerId).Returns((Worker?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Error.NotFound, result.Errors!.First());
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenSalonIsNotFound()
    {
        // Arrange
        var command = new AddWorkerToSalonCommand(Arg.Any<string>());
        var decryptedToken = new WorkerInvitationEncryptionModel
        {
            WorkerId = Id.NewGuid(),
            SalonId = Id.NewGuid()
        };

        _decryptor
            .DecryptTo<WorkerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns(decryptedToken);

        var worker = CreateWorkerWithId(decryptedToken.WorkerId);

        _workersRepository.GetByIdAsync(decryptedToken.WorkerId).Returns(worker);
        _salonsRepository.All.Returns(Enumerable.Empty<Salon>().AsQueryable());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(Error.NotFound, result.Errors!.First());
    }

    [Fact]
    public async Task Handle_ShouldAddWorkerToSalon_WhenWorkerIsNotAlreadyInSalon()
    {
        // Arrange
        var command = new AddWorkerToSalonCommand("valid-token");
        var decryptedToken = new WorkerInvitationEncryptionModel { WorkerId = Guid.NewGuid(), SalonId = Guid.NewGuid() };
        _decryptor.DecryptTo<WorkerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey).Returns(decryptedToken);
        var worker = CreateWorkerWithId(decryptedToken.WorkerId);
        var salon = CreateSalonWithId(decryptedToken.SalonId);
        _workersRepository.GetByIdAsync(decryptedToken.WorkerId).Returns(worker);
        _salonsRepository.All.Returns(new List<Salon> { salon }.AsQueryable());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Contains(worker, salon.Workers!);
        await _unitOfWork.Received(1).SaveChangesAsync(CancellationToken.None);
    }

    [Fact]
    public async Task Handle_ShouldNotAddWorkerToSalon_WhenWorkerIsAlreadyInSalon()
    {
        // Arrange
        var command = new AddWorkerToSalonCommand(Arg.Any<string>());
        var decryptedToken = new WorkerInvitationEncryptionModel
        {
            WorkerId = Id.NewGuid(),
            SalonId = Id.NewGuid()
        };

        _decryptor
            .DecryptTo<WorkerInvitationEncryptionModel>(command.Token, _emailOptions.Value.EncryptionKey)
            .Returns(decryptedToken);

        var worker = CreateWorkerWithId(decryptedToken.WorkerId);
        var salon = CreateSalonWithId(decryptedToken.SalonId);


        salon.Workers = new List<Worker> { worker };

        _workersRepository.GetByIdAsync(decryptedToken.WorkerId).Returns(worker);
        _salonsRepository.All.Returns(new List<Salon> { salon }.AsQueryable());

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(1, salon.Workers.Count);
        await _unitOfWork.DidNotReceive().SaveChangesAsync(CancellationToken.None);
    }
}

