using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.Domain.Users;

namespace SmartSalon.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration config)
        => services
            .RegisterDbContext(config)
            .RegisterIdentity();

    public static IServiceCollection RegisterDbContext(this IServiceCollection services, IConfiguration config)
        => services.AddDbContext<SmartSalonDbContext>(
            options => options.UseSqlServer(config.GetConnectionString("Sql"))
        );

    public static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;

                options.User.RequireUniqueEmail = true;

                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<SmartSalonDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}