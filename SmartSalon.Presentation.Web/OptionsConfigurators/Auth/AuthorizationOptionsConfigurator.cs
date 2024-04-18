using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SmartSalon.Application.Abstractions.Lifetime;
using SmartSalon.Application.Extensions;
using SmartSalon.Presentation.Web.Controllers;

namespace SmartSalon.Presentation.Web.Options.Auth;

public class AuthorizationOptionsConfigurator : IConfigureOptions<AuthorizationOptions>, ITransientLifetime
{
    public void Configure(AuthorizationOptions options)
        => typeof(ApiController)
            .Assembly
            .GetTypes()
            .Where(type => typeof(IAuthorizationRequirement).IsAssignableFrom(type))
            .ForEach(requirement =>
            {
                var policyName = requirement.Name.Replace("Requirement", "Policy");
                var requirementInstance = Activator.CreateInstance(requirement)!.CastTo<IAuthorizationRequirement>();

                options.AddPolicy(policyName, options => options.AddRequirements(requirementInstance));
            });
}
