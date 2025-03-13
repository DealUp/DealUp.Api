using DealUp.Database.Repositories.User;
using DealUp.Domain.Email.Interfaces;
using DealUp.Domain.Identity.Interfaces;
using DealUp.Domain.User.Interfaces;
using DealUp.EmailSender.Extensions;
using DealUp.Infrastructure.Handlers;
using DealUp.Services.Email;
using DealUp.Services.Identity;
using DealUp.Services.User;
using Microsoft.AspNetCore.Authorization;

namespace DealUp.Application.Api.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .AddIdentityServices()
            .AddUserServices()
            .AddEmailSendingServices(configuration);
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddHttpContextAccessor()
            .AddTransient<IHttpContextService, HttpContextService>()
            .AddTransient<IAuthService, AuthService>()
            .AddTransient<IAuthorizationHandler, UserStatusHandler>();
    }

    private static IServiceCollection AddUserServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTransient<IUserService, UserService>()
            .AddTransient<IUserRepository, UserRepository>();
    }

    private static IServiceCollection AddEmailSendingServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .AddTransient<IEmailSendingService, EmailSendingService>()
            .AddEmailInfrastructure(configuration);
    }
}