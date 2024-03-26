using SmartSalon.Presentation.Web;
using SmartSalon.Data;
using SmartSalon.Presentation.Web.Extensions;
using SmartSalon.Application;
using SmartSalon.Application.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var dataLayer = typeof(SmartSalonDbContext).Assembly;
var applicationLayer = typeof(IApplicationLayerMarker).Assembly;
var presentationLayer = typeof(WebConstants).Assembly;

builder.SetupConfigurationFiles();
builder.ConfigureSerilogFromTheConfigurationFiles();

builder
    .Services
    .AddVersioning()
    .AddEndpointsApiExplorer()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddIdentity()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorizationPolicies()
    .AddApplicationServices(builder.Configuration)
    .AddCorsPolicies()
    .AddHttpContextAccessor()
    .RegisterDbContext(builder.Configuration)
    .RegisterTheOptionsClasses(builder.Configuration)
    .RegisterSeedingServices()
    .RegisterConventionalServicesFrom(applicationLayer, dataLayer)
    .RegisterMappingsFrom(applicationLayer, dataLayer, presentationLayer)
    .RegisterInvalidModelStateResponseFactory()
    .AddSwaggerGeneration();

var app = builder.Build();

app
    .UseCors()
    .AddSwaggerUI(app.Environment, app.Services)
    .UseSerilogRequestLogging()
    .UseHttpsRedirection()
    .UseAuthorization()
    .UseExceptionHandling(app.Environment);

app.MapControllers();

app
    .SeedTheDatabase(app.Services)
    .OpenSwaggerOnStartup();

//Only for improved dev experience and will be removed eventually
Console.WriteLine($"Running on: http://localhost:5054");
app.Run();
