using SmartSalon.Presentation.Web;
using SmartSalon.Data;
using SmartSalon.Shared.Extensions;
using SmartSalon.Presentation.Web.Extensions;
using SmartSalon.Application;

var builder = WebApplication.CreateBuilder(args);

var dataAssembly = typeof(SmartSalonDbContext).GetAssembly();
var servicesAssembly = typeof(ApplicationConstants).GetAssembly();
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
