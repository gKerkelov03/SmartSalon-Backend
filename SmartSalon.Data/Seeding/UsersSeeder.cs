using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using SmartSalon.Application.Domain;

namespace SmartSalon.Data.Seeding;

internal class UsersSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var password = "password";

        foreach (var admin in GetAdminsToSeed())
        {
            await userManager.CreateAsync(admin, password);
            await userManager.AddToRoleAsync(admin, AdminRoleName);
        };

        foreach (var customer in GetCustomersToSeed())
        {
            await userManager.CreateAsync(customer, password);
            await userManager.AddToRoleAsync(customer, CustomerRoleName);
        };

        foreach (var worker in GetWorkersToSeed())
        {
            await userManager.CreateAsync(worker, password);
            await userManager.AddToRoleAsync(worker, OwnerRoleName);
        };

        foreach (var owner in GetOwnersToSeed())
        {
            await userManager.CreateAsync(owner, password);
            await userManager.AddToRoleAsync(owner, OwnerRoleName);

        };
    }

    private IEnumerable<Customer> GetCustomersToSeed() => [
        new() {
                FirstName = "Ivan",
                LastName = "Stefanov",
                UserName= "Ivo",
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
                UserName= "Ganio",
                PhoneNumber = "1234567890",
                Email = "gancho@abv.bg",
                IsDeleted = false,
        },
        new()
        {
                FirstName = "Shabi",
                LastName = "Shalmani",
                UserName= "Shabi",
                PhoneNumber = "1234567890",
                Email = "shabi@abv.bg",
                IsDeleted = false,
        }
    ];

    private IEnumerable<Owner> GetOwnersToSeed() => [
        new()
        {
                FirstName = "Mladen",
                LastName = "Petrov",
                UserName= "Mlado",
                PhoneNumber = "1234567890",
                Email = "mladen@abv.bg",
                IsDeleted = false,
        }
    ];

    private IEnumerable<User> GetAdminsToSeed() => [
        new()
        {
            FirstName = "Georgi",
            LastName = "Kerkelov",
            UserName= "Kerkelov",
            PhoneNumber = "0895105609",
            Email = "gkerkelov03@abv.bg",
            IsDeleted = false,
        },
        new()
        {
            FirstName = "Petar",
            LastName = "Ivanov",
            UserName= "Peca",
            PhoneNumber = "0899829897",
            Email = "pivanov03@abv.bg",
            IsDeleted = false,
        }
    ];
}

