using SmartSalon.Web;
using SmartSalon.Data;
using SmartSalon.Services;
using SmartSalon.Shared.Extensions;
using SmartSalon.Web.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var dataAssembly = typeof(SmartSalonDbContext).GetAssembly();
var servicesAssembly = typeof(ServicesConstants).GetAssembly();
var webAssembly = typeof(WebConstants).GetAssembly();

builder.SetupConfigurationFiles();
builder
    .Services
    .AddVersioning()
    .AddEndpointsApiExplorer()

    .RegisterDbContext(builder.Configuration)
    .RegisterSettingsProvider(builder.Configuration)
    .RegisterSeedingServices()
    .RegisterConventionalServicesFrom(servicesAssembly, dataAssembly)
    .RegisterMappingsFrom(servicesAssembly, dataAssembly, webAssembly)

    .ConfigureOptions<SwaggerGenOptionsConfigurator>()
    .AddSwaggerGeneration()

    .AddIdentity()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorizationPolicies()

    .AddCorsPolicies();

var app = builder.Build();

app
    .UseCors()
    .UseDeveloperExceptionPage()
    .UseHttpsRedirection()
    .UseAuthorization()
    .AddSwaggerUI(app.Environment, app.Services)
    .AddSeeding(app.Services);

app.MapControllers();

app.OpenSwaggerOnStartup();
app.Run();

