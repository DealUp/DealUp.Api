using DealUp.Domain.Auth.Interfaces;
using DealUp.Domain.User;
using DealUp.Infrastructure;
using DealUp.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace DealUp.Application.Api.Extensions;

public static class ConfigureServicesExtensions
{
    public static IMvcBuilder SetCamelCaseResponse(this IMvcBuilder builder)
    {
        return builder.AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        });
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}