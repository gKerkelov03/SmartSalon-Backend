using SmartSalon.Presentation.Web;
using SmartSalon.Data;
using SmartSalon.Data.Extensions;
using SmartSalon.Integrations.Extensions;
using SmartSalon.Presentation.Web.Extensions;
using SmartSalon.Application.Extensions;
using Serilog;
using SmartSalon.Integrations.Emails;
using System.Reflection;
using SmartSalon.Application;

var dataLayer = typeof(SmartSalonDbContext).Assembly;
var applicationLayer = typeof(ApplicationConstants).Assembly;
var presentationLayer = typeof(WebConstants).Assembly;
var integrationsLayer = typeof(EmailsManager).Assembly;
var layers = new Assembly[] { dataLayer, applicationLayer, integrationsLayer, presentationLayer };

var builder = WebApplication.CreateBuilder(args);

builder
    .SetupConfigurationFiles()
    .ConfigureSerilog();

builder
    .Services
    .AddApplication(builder.Configuration, layers)
    .AddData(builder.Configuration)
    .AddIntegrations(builder.Configuration)
    .ConfigureAllOptionsClasses(builder.Configuration, layers)
    .CallAddControllers()
    .RegisterServices(layers)

    .AddAuth(builder.Configuration)
    .AddHttpContextAccessor()
    .AddHttpClient()
    .AddCors()
    .AddSwaggerGen()
    .AddVersioning()
    .AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

//TODO: this middleware exists only because of an assignment
app.UseMiddleware<RequestTimingMiddleware>();

app
    .UseExceptionHandling(app.Environment)
    .UseCors(ReactAndAngularLocalhostCorsPolicy)
    .UseSwagger(app.Environment, app.Services)
    .UseAuthentication()
    .UseAuthorization()
    .UseSerilogRequestLogging()
    .UseHttpsRedirection();

app.MapControllers();

app
    .MigrateTheDatabase(app.Services)
    .OpenSwaggerOnStartup();

app.Run();