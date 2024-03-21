using SmartSalon.Presentation.Web;
using SmartSalon.Data;
using SmartSalon.Presentation.Web.Extensions;
using SmartSalon.Application;
using SmartSalon.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);

var dataLayer = typeof(SmartSalonDbContext).GetAssembly();
var applicationLayer = typeof(IApplicationLayerMarker).GetAssembly();
var presentationLayer = typeof(WebConstants).GetAssembly();

builder.SetupConfigurationFiles();
builder
    .Services
    .AddVersioning()
    .AddEndpointsApiExplorer()

    .RegisterDbContext(builder.Configuration)
    .RegisterSettingsProvider(builder.Configuration)
    .RegisterSeedingServices()
    .RegisterConventionalServicesFrom(applicationLayer, dataLayer)
    .RegisterMappingsFrom(applicationLayer, dataLayer, presentationLayer)
    // .RegisterActionFilters()
    .AddApplication()

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
