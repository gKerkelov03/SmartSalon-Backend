using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartSalon.Application.Options;

namespace SmartSalon.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntegrations(this IServiceCollection services, IConfiguration config)
        => services.AddEmailSending(config);

    public static IServiceCollection AddEmailSending(this IServiceCollection services, IConfiguration config)
    {
        var emailsOptions = config.GetSection(EmailsOptions.SectionName).Get<EmailsOptions>();

        if (emailsOptions is null)
        {
            throw new InvalidOperationException($"The section {EmailsOptions.SectionName} is missing from the settings files");
        }

        services
            .AddFluentEmail(emailsOptions.Email)
            .AddRazorRenderer()
            .AddSmtpSender(() =>
                new()
                {
                    Host = emailsOptions.Host,
                    Port = emailsOptions.Port,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(emailsOptions.Email, emailsOptions.Password),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                }
            );

        return services;
    }
}