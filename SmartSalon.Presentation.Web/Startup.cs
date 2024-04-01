using SmartSalon.Presentation.Web;
using SmartSalon.Data;
using SmartSalon.Presentation.Web.Extensions;
using SmartSalon.Application.Extensions;
using Serilog;
using SmartSalon.Application.ResultObject;
using SmartSalon.Integrations.Emails;

var dataLayer = typeof(SmartSalonDbContext).Assembly;
var applicationLayer = typeof(IResult).Assembly;
var presentationLayer = typeof(WebConstants).Assembly;
var integrationsLayer = typeof(EmailsManager).Assembly;

var builder = WebApplication.CreateBuilder(args);

builder
    .SetupConfigurationFiles()
    .ConfigureSerilog();

builder
    .Services
    .AddVersioning()
    .AddEndpointsApiExplorer()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddIdentity()
    .AddJwtAuthentication(builder.Configuration)
    .AddAuthorizationPolicies()
    .AddApplication(builder.Configuration)
    .AddIntegrations(builder.Configuration)
    .AddCorsPolicies()
    .AddHttpContextAccessor()
    .RegisterDbContext(builder.Configuration)
    .RegisterTheOptionsClasses(builder.Configuration)
    .RegisterConventionalServicesFrom(applicationLayer, dataLayer, integrationsLayer)
    .RegisterUnconventionalServices()
    .RegisterMappings(applicationLayer, dataLayer, presentationLayer)
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
