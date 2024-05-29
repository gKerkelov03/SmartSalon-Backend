
using Microsoft.AspNetCore.Identity;
using static SmartSalon.Data.SeedingData.UsersSeedingData;
using static SmartSalon.Data.SeedingData.RolesSeedingData;


namespace SmartSalon.Data.SeedingData;

internal static class UserRolesSeedingData
{
    public static IdentityUserRole<Id>[] Data =
    [
        new()
        {
            UserId = FirstAdminId,
            RoleId = AdminRoleId,
        },
        new()
        {
            UserId = SecondAdminId,
            RoleId = AdminRoleId,
        },
    ];
}

