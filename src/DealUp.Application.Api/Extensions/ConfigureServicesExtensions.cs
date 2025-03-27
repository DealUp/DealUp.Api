using DealUp.Database.Repositories.Advertisement;
using DealUp.Database.Repositories.Seller;
using DealUp.Database.Repositories.User;
using DealUp.Domain.Advertisement.Interfaces;
using DealUp.Domain.Email.Interfaces;
using DealUp.Domain.Identity.Interfaces;
using DealUp.Domain.Seller.Interfaces;
using DealUp.Domain.User.Interfaces;
using DealUp.EmailSender.Extensions;
using DealUp.Infrastructure.Handlers;
using DealUp.Services.Advertisement;
using DealUp.Services.Email;
using DealUp.Services.Identity;
using DealUp.Services.Identity.SsoProviders;
using DealUp.Services.Seller;
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
            .AddEmailSendingServices(configuration)
            .AddSellerServices()
            .AddAdvertisementServices();
    }

    private static IServiceCollection AddIdentityServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddHttpContextAccessor()
            .AddSsoProviders()
            .AddTransient<IHttpContextService, HttpContextService>()
            .AddTransient<IAuthService, AuthService>()
            .AddTransient<IAuthorizationHandler, UserStatusHandler>();
    }

    private static IServiceCollection AddSsoProviders(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTransient<ISsoServiceFactory, SsoServiceFactory>()
            .AddTransient<ISsoProviderService, GoogleSsoService>();
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

    private static IServiceCollection AddSellerServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTransient<ISellerService, SellerService>()
            .AddTransient<ISellerRepository, SellerRepository>();
    }

    private static IServiceCollection AddAdvertisementServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddTransient<IAdvertisementService, AdvertisementService>()
            .AddTransient<IAdvertisementRepository, AdvertisementRepository>();
    }
}