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
        var senderEmail = "barbers.baybg@gmail.com";

        services
            .AddFluentEmail(senderEmail)
            .AddRazorRenderer()
            .AddSmtpSender(() =>
                new()
                {
                    Host = emailsOptions!.SmtpHost,
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, emailsOptions!.Password),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                }
            );

        return services;
    }
}