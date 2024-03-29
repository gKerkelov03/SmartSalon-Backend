using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain;

namespace SmartSalon.Data.Seeding;

internal class UsersSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var userUserManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var password = "password";

        foreach (var admin in GetAdminsToSeed())
        {
            await userUserManager.CreateAsync(admin, password);
            dbContext.SaveChanges();
            await userUserManager.AddToRoleAsync(admin, AdminRoleName);
        };

        foreach (var customer in GetCustomersToSeed())
        {
            await userUserManager.CreateAsync(customer, password);
            await userUserManager.AddToRoleAsync(customer, CustomerRoleName);
        };

        foreach (var worker in GetWorkersToSeed())
        {
            await userUserManager.CreateAsync(worker, password);
            await userUserManager.AddToRoleAsync(worker, OwnerRoleName);
        };

        foreach (var owner in GetOwnersToSeed())
        {
            await userUserManager.CreateAsync(owner, password);
            await userUserManager.AddToRoleAsync(owner, OwnerRoleName);

        };


        dbContext.SaveChanges();
    }

    private IEnumerable<Customer> GetCustomersToSeed() => [
        new() {
                FirstName = "Ivan",
                LastName = "Stefanov",
                PhoneNumber = "1234567890",
                Email = "ivan@abv.bg",
                IsDeleted = false,
        }
    ];

    private IEnumerable<Worker> GetWorkersToSeed() => [
        new()
        {
                FirstName = "Gancho",
                LastName = "Papazov",
                PhoneNumber = "1234567890",
                Email = "gancho@abv.bg",
                IsDeleted = false,
                RoleId = Id.NewGuid(),
        },
        new()
        {
                FirstName = "Shabi",
                LastName = "Shalmani",
                PhoneNumber = "1234567890",
                Email = "shabi@abv.bg",
                IsDeleted = false,
                RoleId = Id.NewGuid(),
        }
    ];

    private IEnumerable<Owner> GetOwnersToSeed() => [
        new()
        {
                FirstName = "Mladen",
                LastName = "Petrov",
                PhoneNumber = "1234567890",
                Email = "mladen@abv.bg",
                IsDeleted = false,
                RoleId = Id.NewGuid(),
        }
    ];

    private IEnumerable<User> GetAdminsToSeed() => [
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

