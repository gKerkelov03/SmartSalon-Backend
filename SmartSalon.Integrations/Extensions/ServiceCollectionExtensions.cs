using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.DependencyInjection;

namespace SmartSalon.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIntegrations(this IServiceCollection services)
        => services.AddEmailSending();

    public static IServiceCollection AddEmailSending(this IServiceCollection services)
    {
        var senderEmail = "barbers.baybg@gmail.com";
        var senderPassword = "dngd roit xigx pmmk";
        var smtpHost = "smtp.gmail.com";

        services
            .AddFluentEmail(senderEmail)
            .AddRazorRenderer()
            .AddSmtpSender(() =>
                new()
                {
                    Host = smtpHost,
                    Port = 587,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                }
            );

        return services;
    }
}