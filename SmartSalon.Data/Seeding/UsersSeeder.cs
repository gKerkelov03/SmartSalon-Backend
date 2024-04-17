global using UsersManager = Microsoft.AspNetCore.Identity.UserManager<SmartSalon.Application.Domain.Users.User>;
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Data.Seeding;

internal class UsersSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var usersManager = serviceProvider.GetRequiredService<UsersManager>();

        foreach (var admin in GetAdminsToSeed())
        {
            await CreateUserAsync(usersManager, admin, AdminRoleName);
        };

        foreach (var customer in GetCustomersToSeed())
        {
            await CreateUserAsync(usersManager, customer, CustomerRoleName);
        };

        foreach (var worker in GetWorkersToSeed())
        {
            await CreateUserAsync(usersManager, worker, WorkerRoleName);
        };

        foreach (var owner in GetOwnersToSeed())
        {
            await CreateUserAsync(usersManager, owner, OwnerRoleName);
        };
    }

    private async Task CreateUserAsync(UsersManager usersManager, User user, string role)
    {
        var password = "Password1";

        await usersManager.CreateAsync(user, password);
        await usersManager.AddToRoleAsync(user, role);

        var _ = await usersManager.GenerateEmailConfirmationTokenAsync(user);
        await usersManager.ConfirmEmailAsync(user, _);
    }

    private IEnumerable<Customer> GetCustomersToSeed() => [
        new() {
            FirstName = "Ivan",
            LastName = "Stefanov",
            PhoneNumber = "1234567890",
            UserName= "ivan@abv.bg",
            Email = "ivan@abv.bg",
            EmailConfirmed = true,
        }
    ];

    private IEnumerable<Worker> GetWorkersToSeed() => [
        new()
        {
            FirstName = "Gancho",
            LastName = "Papazov",
            JobTitle = "Barber",
            PhoneNumber = "1234567890",
            Nickname = "Ganio",
            UserName = "gkerkelov031@gmail.com",
            Email = "gkerkelov031@gmail.com",
            ProfilePictureUrl = "https://res.cloudinary.com/donhvedgr/image/upload/v1663928763/mlnxqpbwatsjlobnqbwl.jpg"
        },
        new()
        {
            FirstName = "Shabi",
            LastName = "Shalmani",
            JobTitle = "Barber",
            PhoneNumber = "1234567890",
            Nickname = "Shabi",
            UserName= "shabi@abv.bg",
            Email = "shabi@abv.bg",
            ProfilePictureUrl = "https://res.cloudinary.com/donhvedgr/image/upload/v1663928319/l9hpu65c6t6mkbaktz3e.jpg"
        }
    ];

    private IEnumerable<Owner> GetOwnersToSeed() => [
        new()
        {
            FirstName = "Stanislav",
            LastName = "Mitkov",
            PhoneNumber = "1234567890",
            UserName= "gkerkelov032@abv.bg",
            Email = "gkerkelov032@abv.bg",
            ProfilePictureUrl = "https://res.cloudinary.com/donhvedgr/image/upload/v1663915155/gaxbdvdgfxqklct4ezpm.jpg"
        },
        new()
        {
            FirstName = "Mladen",
            LastName = "Petrov",
            PhoneNumber = "1234567890",
            UserName= "mladen@abv.bg",
            Email = "mladen@abv.bg",
            ProfilePictureUrl = "https://res.cloudinary.com/donhvedgr/image/upload/v1663915155/gaxbdvdgfxqklct4ezpm.jpg"
        },
    ];

    private IEnumerable<User> GetAdminsToSeed() => [
        new()
        {
            FirstName = "Georgi",
            LastName = "Kerkelov",
            PhoneNumber = "0895105609",
            UserName= "gkerkelov03@abv.bg",
            Email = "gkerkelov03@abv.bg",
        },
        new()
        {
            FirstName = "Petar",
            LastName = "Ivanov",
            PhoneNumber = "0899829897",
            UserName= "pivanov03@abv.bg",
            Email = "pivanov03@abv.bg"
        }
    ];
}

