global using RolesManager = Microsoft.AspNetCore.Identity.RoleManager<SmartSalon.Application.Domain.Users.Role>;
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.Domain.Users;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Data.Seeding;

internal class RolesSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RolesManager>();

        await Task.WhenAll(
            SeedRoleAsync(roleManager, AdminRoleName),
            SeedRoleAsync(roleManager, CustomerRoleName),
            SeedRoleAsync(roleManager, WorkerRoleName),
            SeedRoleAsync(roleManager, OwnerRoleName)
        );
    }

    private static async Task SeedRoleAsync(RolesManager roleManager, string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);

        if (role is null)
        {
            var result = await roleManager.CreateAsync(new Role(roleName));

            if (!result.Succeeded)
            {
                throw new Exception(result.ErrorDescription());
            }
        }
    }
}
