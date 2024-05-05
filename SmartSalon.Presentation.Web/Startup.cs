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
    .AddApplication(builder.Configuration)
    .AddIntegrations(builder.Configuration)
    .ConfigureAllOptionsClasses(builder.Configuration, applicationLayer)
    .CallAddControllers()

    .RegisterConventionalServices(presentationLayer, applicationLayer, dataLayer, integrationsLayer)
    .RegisterUnconventionalServices()
    .RegisterDbContext(builder.Configuration)
    .RegisterIdentityServices()
    .RegisterMapper(applicationLayer, dataLayer, presentationLayer)

    .AddVersioning()
    .AddSwaggerGen()
    .AddExceptionHandler<GlobalExceptionHandler>()
    .AddAuth(builder.Configuration)
    .AddHttpContextAccessor()
    .AddHttpClient()
    .AddCors();

var app = builder.Build();

app
    .UseExceptionHandling(app.Environment)
    .UseCors(AngularLocalhostCorsPolicy)
    .UseSwagger(app.Environment, app.Services)
    .UseAuthentication()
    .UseAuthorization()
    .UseSerilogRequestLogging()
    .UseHttpsRedirection();

app.MapControllers();

app
    .MigrateTheDatabase(app.Services)
    .OpenSwaggerOnStartup();

Console.WriteLine($"Running on: http://localhost:5054");
app.Run();