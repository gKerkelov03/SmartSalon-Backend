using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Extensions;

namespace SmartSalon.Data.Seeding.Seeders;

internal class RolesSeeder : ISeeder
{
    public async Task SeedAsync(SmartSalonDbContext dbContext, IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

        await SeedRoleAsync(roleManager, AdminRoleName);
        await SeedRoleAsync(roleManager, CustomerRoleName);
        await SeedRoleAsync(roleManager, WorkerRoleName);
        await SeedRoleAsync(roleManager, OwnerRoleName);
    }

    private static async Task SeedRoleAsync(RoleManager<Role> roleManager, string roleName)
    {
        var role = await roleManager.FindByNameAsync(roleName);

        if (role is null)
        {
            var result = await roleManager.CreateAsync(new Role(roleName));

            if (!result.Succeeded)
            {
                throw new Exception(result.GetErrorMessage());
            }
        }
    }
}
