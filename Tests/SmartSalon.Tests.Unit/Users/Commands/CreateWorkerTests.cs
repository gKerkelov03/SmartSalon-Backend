using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Application.ResultObject;
using Xunit;

namespace SmartSalon.Tests.Unit.Users
{
    public class CreateWorkerCommandHandlerTests : TestsClass
    {
        private readonly UsersManager _usersManager;
        private readonly IEfRepository<Salon> _salonsRepository;
        private readonly IJobTitlesRepository _jobTitlesRepository;
        private readonly IMapper _mapper;
        private readonly CreateWorkerCommandHandler _handler;

        public CreateWorkerCommandHandlerTests()
        {
            _usersManager = CreateUsersManager();
            _salonsRepository = Substitute.For<IEfRepository<Salon>>();
            _jobTitlesRepository = Substitute.For<IJobTitlesRepository>();
            _mapper = CreateMapper();
            _handler = new CreateWorkerCommandHandler(_usersManager, _salonsRepository, _jobTitlesRepository, _mapper);
        }

        [Fact]
        public async Task ShouldReturnConflict_WhenEmailAlreadyExists()
        {
            // Arrange
            var command = new CreateWorkerCommand
            {
                Email = "test@example.com",
                FirstName = "Gosho",
                LastName = "Ivan",
                PhoneNumber = "0895105105105",
                JobTitlesIds = [],
                ProfilePictureUrl = "url",
                Password = "StrongPassword103"
            };

            _usersManager.FindByEmailAsync(command.Email).Returns(CreateWorker());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.IsType<ConflictError>(result.Errors!.First());
        }

        [Fact]
        public async Task ShouldReturnNotFound_WhenSalonDoesNotExist()
        {
            // Arrange
            var command = new CreateWorkerCommand
            {
                Email = "test@example.com",
                FirstName = "Gosho",
                LastName = "Ivan",
                PhoneNumber = "0895105105105",
                JobTitlesIds = [],
                ProfilePictureUrl = "url",
                Password = "StrongPassword103",
                SalonId = new Id()
            };

            _usersManager.FindByEmailAsync(command.Email).Returns((User?)null);
            _salonsRepository.GetByIdAsync(command.SalonId).Returns((Salon?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.IsType<NotFoundError>(result.Errors!.First());
        }

        [Fact]
        public async Task ShouldReturnError_WhenJobTitlesAreInvalid()
        {
            // Arrange
            var command = new CreateWorkerCommand
            {

                Email = "test@example.com",
                FirstName = "Gosho",
                LastName = "Ivan",
                PhoneNumber = "0895105105105",
                ProfilePictureUrl = "url",
                Password = "StrongPassword103",
                SalonId = new Id(),
                JobTitlesIds = new List<Id> { new Id() }
            };

            var salon = CreateSalon();
            _usersManager.FindByEmailAsync(command.Email).Returns((User?)null);
            _salonsRepository.GetByIdAsync(command.SalonId).Returns(salon);
            _jobTitlesRepository
                .GetJobTitlesInSalon(command.SalonId, command.JobTitlesIds)
                .Returns(Result<IEnumerable<JobTitle>>.Failure([Error.NotFound]));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.IsType<NotFoundError>(result.Errors!.First());
        }

        [Fact]
        public async Task ShouldReturnError_WhenUserCreationFails()
        {
            // Arrange
            var command = new CreateWorkerCommand
            {
                Email = "test@example.com",
                SalonId = new Id(),
                JobTitlesIds = new List<Id> { new Id() },
                FirstName = "John",
                LastName = "Doe",
                Password = "Password123",
                PhoneNumber = "0895105105105",
                ProfilePictureUrl = "url",
            };

            var salon = CreateSalon();
            var jobTitles = new List<JobTitle> { new() { Name = "Barber" } };
            var identityError = new IdentityError { Description = "User creation failed" };

            _usersManager.FindByEmailAsync(command.Email).Returns((User?)null);
            _salonsRepository.GetByIdAsync(command.SalonId).Returns(salon);
            _jobTitlesRepository
                .GetJobTitlesInSalon(command.SalonId, command.JobTitlesIds)
                .Returns(Result<IEnumerable<JobTitle>>.Success(jobTitles));

            _usersManager.CreateAsync(Arg.Any<User>(), command.Password).Returns(IdentityResult.Failed(identityError));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("User creation failed", result.Errors?.First().Description);
        }

        [Fact]
        public async Task ShouldReturnError_WhenAddingToRoleFails()
        {
            // Arrange
            var command = new CreateWorkerCommand
            {
                Email = "test@example.com",
                SalonId = new Id(),
                JobTitlesIds = new List<Id> { new() },
                FirstName = "John",
                LastName = "Doe",
                Password = "Password123",
                PhoneNumber = "0895105105105",
                ProfilePictureUrl = "url",
            };

            var salon = CreateSalon();
            var jobTitles = new List<JobTitle> { new() { Name = "Barber" } };
            _usersManager.FindByEmailAsync(command.Email).Returns((User?)null);
            _salonsRepository.GetByIdAsync(command.SalonId).Returns(salon);

            _jobTitlesRepository
                .GetJobTitlesInSalon(command.SalonId, command.JobTitlesIds)
                .Returns(Result<IEnumerable<JobTitle>>.Success(jobTitles));

            _usersManager.CreateAsync(Arg.Any<User>(), command.Password).Returns(IdentityResult.Success);
            _usersManager.AddToRoleAsync(Arg.Any<User>(), WorkerRoleName).Returns(IdentityResult.Failed(new IdentityError { Description = "Adding to role failed" }));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Adding to role failed", result.Errors!.First().Description);
        }

        [Fact]
        public async Task ShouldReturnSuccess_WhenWorkerCreatedSuccessfully()
        {
            // Arrange
            var command = new CreateWorkerCommand
            {
                Email = "test@example.com",
                SalonId = new Id(),
                JobTitlesIds = new List<Id> { new Id() },
                FirstName = "John",
                LastName = "Doe",
                Password = "Password123",
                PhoneNumber = "0895105105105",
                ProfilePictureUrl = "url",
            };

            var salon = CreateSalon();
            var jobTitles = new List<JobTitle> { new() { Name = "Barber" } };
            _usersManager.FindByEmailAsync(command.Email).Returns((User?)null);
            _salonsRepository.GetByIdAsync(command.SalonId).Returns(salon);

            _jobTitlesRepository
                .GetJobTitlesInSalon(command.SalonId, command.JobTitlesIds)
                .Returns(Result<IEnumerable<JobTitle>>.Success(jobTitles));

            _usersManager.CreateAsync(Arg.Any<User>(), command.Password).Returns(IdentityResult.Success);
            _usersManager.AddToRoleAsync(Arg.Any<User>(), WorkerRoleName).Returns(IdentityResult.Success);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
        }
    }
}
