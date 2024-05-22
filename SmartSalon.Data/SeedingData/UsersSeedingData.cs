
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Data.SeedingData;

internal static class UsersSeedingData
{
    private static string _password = "Password1";
    private static PasswordHasher _passwordHasher = new();

    public static Id FirstAdminId = Guid.NewGuid();
    public static Id SecondAdminId = Guid.NewGuid();


    public static User[] AdminsData = new User[]
    {
        new()
        {
            Id = FirstAdminId,
            FirstName = "Georgi",
            LastName = "Kerkelov",
            PhoneNumber = "0895105609",
            UserName= "gkerkelov03@abv.bg",
            Email = "gkerkelov03@abv.bg",
        },
        new()
        {
            Id = SecondAdminId,
            FirstName = "Petar",
            LastName = "Ivanov",
            PhoneNumber = "0899829897",
            UserName= "pivanov03@abv.bg",
            Email = "pivanov03@abv.bg",
        }
    }
    .Select(user =>
    {
        user.PasswordHash = _passwordHasher.HashPassword(user, _password);
        user.NormalizedUserName = user.UserName.ToUpper();
        user.NormalizedEmail = user.Email.ToUpper();
        user.SecurityStamp = Guid.NewGuid().ToString();

        return user;
    })
    .ToArray();
}

