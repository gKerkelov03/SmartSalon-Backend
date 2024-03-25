using SmartSalon.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain;

namespace SmartSalon.Data.Seeding.Seeders;

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

    private Customer[] GetCustomersToSeed()
    {
        return [
            new Customer {
                UserProfile = new()
                {
                    FirstName = "Ivan",
                    LastName = "Stefanov",
                    PhoneNumber = "1234567890",
                    Email = "ivan@abv.bg",
                    IsDeleted = false,
                    CreatedOn = DateTime.Now,
                },
            }
        ];
    }

    private Worker[] GetWorkersToSeed()
    {
        var profileId1 = Id.NewGuid();
        var profileId2 = Id.NewGuid();

        return [
            new Worker
            {
                UserProfile = new()
                {
                    FirstName = "Gancho",
                    LastName = "Papazov",
                    PhoneNumber = "1234567890",
                    Email = "gancho@abv.bg",
                    IsDeleted = false,
                    CreatedOn = DateTime.Now,
                    RoleId = Id.NewGuid(),
                    Id = profileId1
                },
                UserProfileId = profileId1
            },
            new Worker
            {
                UserProfile = new()
                {
                    FirstName = "Shabi",
                    LastName = "Shalmani",
                    PhoneNumber = "1234567890",
                    Email = "shabi@abv.bg",
                    IsDeleted = false,
                    CreatedOn = DateTime.Now,
                    RoleId = Id.NewGuid(),
                    Id = profileId2
                },
                UserProfileId = profileId2
            }
        ];
    }

    private Owner[] GetOwnersToSeed()
    {
        var profileId = Id.NewGuid();

        return [
            new Owner
            {
                UserProfile = new()
                {
                    FirstName = "Mladen",
                    LastName = "Petrov",
                    PhoneNumber = "1234567890",
                    Email = "mladen@abv.bg",
                    IsDeleted = false,
                    CreatedOn = DateTime.Now,
                    RoleId = Id.NewGuid(),
                    Id = profileId
                },
                UserProfileId = profileId
            }
        ];
    }

    private UserProfile[] GetAdminsToSeed()
        => [
            new UserProfile
            {
                FirstName = "Georgi",
                LastName = "Kerkelov",
                PhoneNumber = "0895105609",
                Email = "gkerkelov03@abv.bg",
                IsDeleted = false,
                CreatedOn = DateTime.Now,
            },
            new UserProfile
            {
                FirstName = "Petar",
                LastName = "Ivanov",
                PhoneNumber = "0899829897",
                Email = "pivanov03@abv.bg",
                IsDeleted = false,
                CreatedOn = DateTime.Now,
            }
        ];
}

