using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Presentation.Web.Policies;

namespace SmartSalon.Presentation.Web.Options.Auth;

public class AuthorizationOptionsConfigurator : IConfigureOptions<AuthorizationOptions>, ISingletonLifetime
{
    public void Configure(AuthorizationOptions options)
    {
        options.AddPolicy(
            IsAdminPolicy,
            policy => policy.AddRequirements(new IsAdminRequirement())
        );

        options.AddPolicy(
            IsOwnerOrAdminPolicy,
            policy => policy.AddRequirements(new IsOwnerOrAdminRequirement())
        );

        options.AddPolicy(
            IsTheSameUserOrAdminPolicy,
            policy => policy.AddRequirements(new IsTheSameUserOrAdminRequirement())
        );

        options.AddPolicy(
            IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerPolicy,
            policy => policy.AddRequirements(new IsOwnerOfTheSalonOfTheWorkerOrIsTheWorkerRequirement())
        );

        options.AddPolicy(
            IsOwnerOfTheSalonOrIsAdminPolicy,
            policy => policy.AddRequirements(new IsOwnerOfTheSalonOrIsAdminRequirement())
        );
    }
}
