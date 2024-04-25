
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Data.SeedingData;

internal static class RolesSeedingData
{
    public static Id AdminRoleId = Id.NewGuid();
    public static Id CustomerRoleId = Id.NewGuid();
    public static Id OwnerRoleId = Id.NewGuid();
    public static Id WorkerRoleId = Id.NewGuid();

    public static Role[] Data = [
        new(CustomerRoleName){
            Id = CustomerRoleId,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = CustomerRoleName.ToUpper()
        },
        new(OwnerRoleName){
            Id = OwnerRoleId,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = OwnerRoleName.ToUpper()
        },
        new(WorkerRoleName){
            Id = WorkerRoleId,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = WorkerRoleName.ToUpper()
        },
        new(AdminRoleName){
            Id = AdminRoleId,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = AdminRoleName.ToUpper()
        },
    ];
}

