using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Data;
using Xunit;

namespace SmartSalon.Tests.Unit.Users;

public class CreateOwnerTests : TestsClass
{
    private readonly UsersManager _usersManager;
    private readonly IEfRepository<Salon> _salonsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly SmartSalonDbContext _dbContext;
    private readonly CreateOwnerCommandHandler _handler;

    public CreateOwnerTests()
    {
        _usersManager = CreateUsersManager();
        _salonsRepository = Substitute.For<IEfRepository<Salon>>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _mapper = Substitute.For<IMapper>();
        _dbContext = CreateDbContext();

        _handler = new CreateOwnerCommandHandler(_usersManager, _salonsRepository, _unitOfWork, _mapper);
    }

    [Fact]
    public async Task ShouldReturnConflict_WhenUserWithSameEmailExists()
    {
        // Arrange
        var command = new CreateOwnerCommand
        {
            SalonId = Id.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "123456789",
            ProfilePictureUrl = "http://example.com/picture.jpg",
            Email = "existing@example.com",
            Password = "Password123"
        };

        _usersManager.FindByEmailAsync(command.Email)!.Returns(Task.FromResult(CreateUser()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<ConflictError>(result.Errors!.First());
    }

    [Fact]
    public async Task ShouldReturnNotFound_WhenSalonDoesNotExist()
    {
        // Arrange
        var command = new CreateOwnerCommand
        {
            SalonId = Id.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "123456789",
            ProfilePictureUrl = "http://example.com/picture.jpg",
            Email = "new@example.com",
            Password = "Password123"
        };

        _usersManager.FindByEmailAsync(command.Email)!.Returns(Task.FromResult<User?>(null));
        _salonsRepository.All.Returns(_dbContext.Salons);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(Error.NotFound, result.Errors!.First());
    }

    [Fact]
    public async Task ShouldReturnError_WhenCreatingUserFails()
    {
        // Arrange
        var command = new CreateOwnerCommand
        {
            SalonId = Id.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "123456789",
            ProfilePictureUrl = "http://example.com/picture.jpg",
            Email = "new@example.com",
            Password = "Password123"
        };

        var salon = CreateSalonWithId(command.SalonId);
        var identityError = new IdentityError { Description = "Creation failed" };
        var identityResult = IdentityResult.Failed(identityError);

        _dbContext.Salons.Add(salon);
        await _dbContext.SaveChangesAsync();

        _usersManager.FindByEmailAsync(command.Email)!.Returns(Task.FromResult<User?>(null));
        _salonsRepository.All.Returns(_dbContext.Salons);
        _mapper.Map<Owner>(command).Returns(CreateOwner());
        _usersManager.CreateAsync(Arg.Any<Owner>(), command.Password).Returns(Task.FromResult(identityResult));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(
            identityResult.Errors.First().Description,
            result.Errors!.First().Description
        );
    }

    [Fact]
    public async Task ShouldReturnError_WhenAddingUserToRoleFails()
    {
        // Arrange
        var command = new CreateOwnerCommand
        {
            SalonId = Id.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "123456789",
            ProfilePictureUrl = "http://example.com/picture.jpg",
            Email = "new@example.com",
            Password = "Password123"
        };
        var ownerId = Id.NewGuid();
        var salon = CreateSalonWithId(command.SalonId);
        var owner = CreateOwnerWithId(ownerId);
        var identityError = new IdentityError { Description = "Role assignment failed" };
        var identityResult = IdentityResult.Success;
        var roleResult = IdentityResult.Failed(identityError);

        _dbContext.Salons.Add(salon);
        await _dbContext.SaveChangesAsync();

        _usersManager.FindByEmailAsync(command.Email)!.Returns(Task.FromResult<User?>(null));
        _salonsRepository.All.Returns(_dbContext.Salons);
        _mapper.Map<Owner>(command).Returns(owner);
        _usersManager.CreateAsync(owner, command.Password).Returns(Task.FromResult(identityResult));
        _usersManager.AddToRoleAsync(owner, OwnerRoleName).Returns(Task.FromResult(roleResult));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Equal(roleResult.Errors.First().Description, result.Errors!.First().Description);
    }

    [Fact]
    public async Task ShouldCreateOwnerSuccessfully_WhenValid()
    {
        // Arrange
        var command = new CreateOwnerCommand
        {
            SalonId = Id.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "123456789",
            ProfilePictureUrl = "http://example.com/picture.jpg",
            Email = "new@example.com",
            Password = "Password123"
        };
        var ownerId = Id.NewGuid();
        var owner = CreateOwnerWithId(ownerId);
        var salon = CreateSalonWithId(command.SalonId);
        var identityResult = IdentityResult.Success;
        var roleResult = IdentityResult.Success;

        _dbContext.Salons.Add(salon);
        await _dbContext.SaveChangesAsync();

        _usersManager.FindByEmailAsync(command.Email)!.Returns(Task.FromResult<User?>(null));
        _salonsRepository.All.Returns(_dbContext.Salons);
        _mapper.Map<Owner>(command).Returns(owner);
        _usersManager.CreateAsync(owner, command.Password).Returns(Task.FromResult(identityResult));
        _usersManager.AddToRoleAsync(owner, OwnerRoleName).Returns(Task.FromResult(roleResult));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(result.Value.CreatedOwnerId, ownerId);
        await _unitOfWork.Received().SaveChangesAsync(CancellationToken.None);
    }
}
