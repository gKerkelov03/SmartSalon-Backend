using System.Globalization;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartSalon.Application.Abstractions;
using SmartSalon.Application.Domain;
using SmartSalon.Application.Errors;
using SmartSalon.Application.Mapping;
using SmartSalon.Application.ResultObject;
using SmartSalon.Data;
using SmartSalon.Data.Seeding;
using SmartSalon.Presentation.Web.Filters;
using SmartSalon.Presentation.Web.Options;

namespace SmartSalon.Presentation.Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterMappingsFrom(
        this IServiceCollection services,
        params Assembly[] assemblies
    )
    {
        AutoMapperConfig.RegisterMappings(assemblies);
        services.AddSingleton(AutoMapperConfig.MapperInstance);

        return services;
    }

    public static IServiceCollection RegisterDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<SmartSalonDbContext>(options =>
            options.UseSqlServer(
                configuration
                    .GetConnectionString("Sql")
            )
        );

        return services;
    }

    public static IServiceCollection RegisterTheOptionsClasses(this IServiceCollection services, IConfiguration config)
        => services
            .Configure<ConnectionStringsOptions>(config.GetSection("ConnectionStrings"))
            .Configure<SecretKeysOptions>(config.GetSection("ConnectionStrings"))
            .ConfigureOptions<SwaggerGenOptionsConfigurator>();

    public static IServiceCollection RegisterSeedingServices(this IServiceCollection services)
        => services.AddSingleton<ISeeder, SmartSalonDbContextSeeder>();

    public static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<UserProfile, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;

                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<SmartSalonDbContext>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        var jwtSecret = config.GetSection("SecretKeys")?.Get<SecretKeysOptions>()?.Jwt ?? "somedefaultvalue";
        var signingKeyBytes = Encoding.ASCII.GetBytes(jwtSecret);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }

    public static IServiceCollection AddAuthorizationPolicies(
        this IServiceCollection services
    )
        => services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    CustomerPolicyName,
                    policy => policy.RequireRole(CustomerRoleName)
                );

                options.AddPolicy(
                    WorkerPolicyName,
                    policy => policy.RequireRole(WorkerRoleName)
                );

                options.AddPolicy(
                    OwnerPolicyName,
                    policy => policy.RequireRole(OwnerRoleName)
                );

                options.AddPolicy(
                    AdminPolicyName,
                    policy => policy.RequireRole(AdminRoleName)
                );
            });

    public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        => services
            .AddCors(options => options
                .AddPolicy(
                    AngularLocalhostCorsPolicyName,
                    policy => policy
                        .WithOrigins(AngularLocalhostUrl)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                ));

    public static IServiceCollection RegisterConventionalServicesFrom(
        this IServiceCollection services,
        params Assembly[] assemblies
    )
    {
        var iTransientService = typeof(ITransientLifetime);
        var iScopedService = typeof(IScopedLifetime);
        var iSingletonService = typeof(ISingletonLifetime);

        return services.Scan(types =>
        {
            var allTypes = types.FromAssemblies(assemblies);

            allTypes
                .AddClasses(@class => @class.AssignableTo(iScopedService))
                .AsImplementedInterfaces()
                .WithScopedLifetime();

            allTypes
                .AddClasses(@class => @class.AssignableTo(iSingletonService))
                .AsImplementedInterfaces()
                .WithSingletonLifetime();

            allTypes
                .AddClasses(@class => @class.AssignableTo(iTransientService))
                .AsImplementedInterfaces()
                .WithTransientLifetime();
        });
    }

    public static IServiceCollection RegisterActionFilters(this IServiceCollection services)
    {
        services.AddControllers(options =>
            options.Filters.Add<ResultOrErrorActionFilter>());

        return services;
    }

    public static IServiceCollection RegisterInvalidModelStateResponseFactory(this IServiceCollection services)
    {
        services
        .AddControllers()
        .ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory =
            context =>
            {
                var validationErrors = context
                    .ModelState
                    .Where(kvp => kvp.Value?.Errors.Count > 0)
                    .Select(kvp => new { PropertyName = kvp.Key, Errors = kvp.Value!.Errors.Select(error => error.ErrorMessage) })
                    .SelectMany(validationViolations =>
                        validationViolations.Errors.Select(error => Error.Validation(validationViolations.PropertyName, error)));

                var result = Result.Failure(validationErrors);
                var traceId = context.HttpContext.TraceIdentifier;
                return new ObjectResult(result.ToProblemDetails(traceId));
            }
        );

        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
        => services
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddVersionedApiExplorer(options =>
            {
                //VVV means major.minor.patch => v1.2.3
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

    public static IServiceCollection AddSwaggerGeneration(this IServiceCollection services)
        => services.AddSwaggerGen(options =>
        {
            var schemeName = "Bearer";
            var securityScheme = new OpenApiSecurityScheme()
            {
                Reference = new()
                {
                    Id = schemeName,
                    Type = ReferenceType.SecurityScheme
                }
            };

            options.AddSecurityDefinition(schemeName, new()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = schemeName,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = $"""
                    JWT Authorization header using the Bearer scheme.
                    Enter 'Bearer' [space] and then your token 

                    Example: '{schemeName} 1a2b3c4d5e6f7g'
                """,
            });

            options.AddSecurityRequirement(new()
            {
                [securityScheme] = []
            });
        });
}
