using Microsoft.EntityFrameworkCore;
using SmartSalon.Application.Domain.Salons;
using SmartSalon.Application.Domain.Services;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Options;
using SmartSalon.Data;

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
            UserName = ""
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
            Nickname = ""
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
            EncryptionKey = "test-key",
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
}