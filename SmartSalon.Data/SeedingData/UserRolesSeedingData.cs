
using Microsoft.AspNetCore.Identity;

namespace SmartSalon.Data.SeedingData;

internal static class UserRolesSeedingData
{
    public static IdentityUserRole<Id>[] Data =
    [
        new()
        {
            UserId = UsersSeedingData.FirstAdminId,
            RoleId = RolesSeedingData.AdminRoleId,
        },
        new()
        {
            UserId = UsersSeedingData.SecondAdminId,
            RoleId = RolesSeedingData.AdminRoleId,
        },
    ];
}

