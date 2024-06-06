using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using SmartSalon.Application;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Extensions;
using SmartSalon.Application.Features.Users.Commands;
using SmartSalon.Application.Options;
using SmartSalon.Data;
using SmartSalon.Integrations.Emails;
using SmartSalon.Presentation.Web;

public abstract class TestsClass
{
    protected Owner CreateOwnerWithId(Id id)
    {
        var owner = CreateOwner();
        owner.Id = id;

        return owner;
    }

    protected Owner CreateOwner()
        => new()
        {
            FirstName = "",
            LastName = "",
            Email = "",
            ProfilePictureUrl = "",
            PhoneNumber = "",
            UserName = "",
            NormalizedEmail = "",
            NormalizedUserName = "",
            SecurityStamp = "",
        };

    protected Worker CreateWorkerWithId(Id id)
    {
        var worker = CreateWorker();
        worker.Id = id;

        return worker;
    }

    protected Worker CreateWorker()
        => new()
        {
            FirstName = "",
            LastName = "",
            Email = "",
            ProfilePictureUrl = "",
            PhoneNumber = "",
            UserName = "",
            Nickname = "",
            NormalizedEmail = "",
            NormalizedUserName = "",
            SecurityStamp = "",
        };

    protected User CreateUserWithId(Id id)
    {
        var user = CreateUser();
        user.Id = id;
        return user;
    }

    protected User CreateUser()
        => new()
        {
            FirstName = "",
            LastName = "",
            Email = "",
            ProfilePictureUrl = "",
            PhoneNumber = "",
            UserName = "",
            NormalizedEmail = "",
            NormalizedUserName = "",
            SecurityStamp = "",
        };

    protected Salon CreateSalonWithId(Id id)
    {
        var salon = CreateSalon();
        salon.Id = id;

        return salon;
    }

    protected Salon CreateSalon()
        => new()
        {
            OtherAcceptedCurrencies = new List<Currency>(),
            JobTitles = new List<JobTitle>(),
            Sections = new List<Section>(),
            Images = new List<Image>(),
            Specialties = new List<Specialty>(),
            Workers = new List<Worker>(),
            Owners = new List<Owner>(),
            Name = "",
            Latitude = "",
            Longitude = "",
            Country = "",
            Description = "",
            GoogleMapsLocation = ""
        };

    protected EmailOptions CreateEmailOptions()
        => new()
        {
            EncryptionKey = "",
            Email = "",
            Password = "",
            Host = "",
            Port = 0
        };

    protected SmartSalonDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<SmartSalonDbContext>()
            .UseInMemoryDatabase(databaseName: "TestSmartSalonDatabase")
            .Options;

        return new SmartSalonDbContext(options);
    }

    protected UsersManager CreateUsersManager()
    {
        var userStore = Substitute.For<IUserStore<User>>();
        var identityOptions = Substitute.For<IOptions<IdentityOptions>>();
        var passwordHasher = new PasswordHasher<User>();
        var userValidators = new List<IUserValidator<User>> { new UserValidator<User>() };
        var passwordValidators = new List<IPasswordValidator<User>> { new PasswordValidator<User>() };
        var keyNormalizer = Substitute.For<ILookupNormalizer>();
        var errors = new IdentityErrorDescriber();
        var services = Substitute.For<IServiceProvider>();
        var logger = Substitute.For<ILogger<UsersManager>>();

        identityOptions.Value.Returns(new IdentityOptions());

        var userManager = Substitute.For<UsersManager>(
            userStore,
            identityOptions,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger);

        return userManager;
    }

    protected IMapper CreateMapper()
    {
        var dataLayer = typeof(SmartSalonDbContext).Assembly;
        var applicationLayer = typeof(ApplicationConstants).Assembly;
        var presentationLayer = typeof(WebConstants).Assembly;
        var integrationsLayer = typeof(EmailsManager).Assembly;

        var services = new ServiceCollection();

        services.RegisterMapper(dataLayer, applicationLayer, presentationLayer, integrationsLayer);
        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IMapper>();
    }
}