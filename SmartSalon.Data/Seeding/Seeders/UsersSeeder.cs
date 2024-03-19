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

        await GetCustomersToSeed().ForEachAsync(async customer =>
        {
            var profile = customer.UserProfile!;
            await userProfileManager.CreateAsync(profile, password);
            await userProfileManager.AddToRoleAsync(profile, CustomerRoleName);
        });

        await GetWorkersToSeed().ForEachAsync(async worker =>
        {
            var profile = worker.UserProfile!;
            await userProfileManager.CreateAsync(profile, password);
            await userProfileManager.AddToRoleAsync(profile, OwnerRoleName);

        });

        await GetOwnersToSeed().ForEachAsync(async owner =>
        {
            var profile = owner.UserProfile!;
            await userProfileManager.CreateAsync(profile, password);
            await userProfileManager.AddToRoleAsync(profile, OwnerRoleName);

        });

        await GetAdminsToSeed().ForEachAsync(async admin =>
        {
            await userProfileManager.CreateAsync(admin, password);
            await userProfileManager.AddToRoleAsync(admin, AdminRoleName);
        });

        dbContext.SaveChanges();
    }

    private Customer[] GetCustomersToSeed()
    {
        var profileId = Id.NewGuid();

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
                    RoleId = Id.NewGuid(),
                    Id = profileId
                },
                UserProfileId = profileId
            }
        ];
    }

    private Worker[] GetWorkersToSeed()
    {
        var profileId = Id.NewGuid();

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
                    Id = profileId
                },
                UserProfileId = profileId
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
                    Id = profileId
                },
                UserProfileId = profileId
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
                RoleId = Id.NewGuid(),
            },
            new UserProfile
            {
                FirstName = "Petar",
                LastName = "Ivanov",
                PhoneNumber = "0899829897",
                Email = "pivanov03@abv.bg",
                IsDeleted = false,
                CreatedOn = DateTime.Now,
                RoleId = Id.NewGuid(),
            }
        ];
}

