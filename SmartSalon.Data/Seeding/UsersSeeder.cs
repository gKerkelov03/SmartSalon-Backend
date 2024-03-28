using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain;

namespace SmartSalon.Data.Seeding;

internal class UsersSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var userProfileManager = serviceProvider.GetRequiredService<UserManager<UserProfile>>();
        var password = "password";

        foreach (var admin in GetAdminsToSeed())
        {
            await userProfileManager.CreateAsync(admin, password);
            dbContext.SaveChanges();
            await userProfileManager.AddToRoleAsync(admin, AdminRoleName);
        };

        foreach (var customer in GetCustomersToSeed())
        {
            var profile = customer.UserProfile!;
            await userProfileManager.CreateAsync(profile, password);
            await userProfileManager.AddToRoleAsync(profile, CustomerRoleName);
        };

        foreach (var worker in GetWorkersToSeed())
        {
            var profile = worker.UserProfile!;
            await userProfileManager.CreateAsync(profile, password);
            await userProfileManager.AddToRoleAsync(profile, OwnerRoleName);
        };

        foreach (var owner in GetOwnersToSeed())
        {
            var profile = owner.UserProfile!;
            await userProfileManager.CreateAsync(profile, password);
            await userProfileManager.AddToRoleAsync(profile, OwnerRoleName);

        };


        dbContext.SaveChanges();
    }

    private IEnumerable<Customer> GetCustomersToSeed() => [
        new() {
            UserProfile = new()
            {
                FirstName = "Ivan",
                LastName = "Stefanov",
                PhoneNumber = "1234567890",
                Email = "ivan@abv.bg",
                IsDeleted = false,
            },
        }
    ];

    private IEnumerable<Worker> GetWorkersToSeed() => [
        new()
        {
            UserProfile = new()
            {
                FirstName = "Gancho",
                LastName = "Papazov",
                PhoneNumber = "1234567890",
                Email = "gancho@abv.bg",
                IsDeleted = false,
                RoleId = Id.NewGuid(),
            },
        },
        new()
        {
            UserProfile = new()
            {
                FirstName = "Shabi",
                LastName = "Shalmani",
                PhoneNumber = "1234567890",
                Email = "shabi@abv.bg",
                IsDeleted = false,
                RoleId = Id.NewGuid(),
            },
        }
    ];

    private IEnumerable<Owner> GetOwnersToSeed() => [
        new()
        {
            UserProfile = new()
            {
                FirstName = "Mladen",
                LastName = "Petrov",
                PhoneNumber = "1234567890",
                Email = "mladen@abv.bg",
                IsDeleted = false,
                RoleId = Id.NewGuid(),
            },
        }
    ];

    private IEnumerable<UserProfile> GetAdminsToSeed() => [
        new()
        {
            FirstName = "Georgi",
            LastName = "Kerkelov",
            PhoneNumber = "0895105609",
            Email = "gkerkelov03@abv.bg",
            IsDeleted = false,
        },
        new()
        {
            FirstName = "Petar",
            LastName = "Ivanov",
            PhoneNumber = "0899829897",
            Email = "pivanov03@abv.bg",
            IsDeleted = false,
        }
    ];
}

