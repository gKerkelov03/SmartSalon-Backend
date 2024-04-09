using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace SmartSalon.Presentation.Web.Options.Auth;

public class AuthorizationOptionsConfigurator : IConfigureOptions<AuthorizationOptions>
{
    public void Configure(AuthorizationOptions options)
    {
        options.AddPolicy(CustomerPolicyName, policy => policy.RequireRole(CustomerRoleName));
        options.AddPolicy(WorkerPolicyName, policy => policy.RequireRole(WorkerRoleName));
        options.AddPolicy(OwnerPolicyName, policy => policy.RequireRole(OwnerRoleName));
        options.AddPolicy(AdminPolicyName, policy => policy.RequireRole(AdminRoleName));
    }
}


