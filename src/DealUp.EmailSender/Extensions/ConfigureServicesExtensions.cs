using DealUp.EmailSender.Configuration;
using DealUp.EmailSender.Interfaces;
using DealUp.EmailSender.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace DealUp.EmailSender.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddEmailInfrastructure(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .Configure<EmailSendingOptions>(configuration.GetSection(EmailSendingOptions.SectionName))
            .AddTransient<IEmailSenderFactory, EmailSenderFactory>()
            .AddTransient<IEmailSender, ResendEmailSender>()
            .AddResendClient(configuration);
    }

    private static IServiceCollection AddResendClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .Configure<ResendClientOptions>(configuration.GetSection($"{EmailSendingOptions.SectionName}:{nameof(ResendClientOptions)}"))
            .AddHttpClient<IResend, ResendClient>().Services;
    }
}