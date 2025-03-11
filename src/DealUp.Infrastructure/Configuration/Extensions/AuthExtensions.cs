using DealUp.Infrastructure.Configuration.Options;
using DealUp.Infrastructure.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DealUp.Infrastructure.Configuration.Extensions;

public static class AuthExtensions
{
    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddJwtOptions(builder.Configuration)
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtOptions = builder.Configuration.GetJwtOptionsSection().Get<JwtOptions>()!;

                options.RequireHttpsMetadata = false;
                options.TokenHandlers.Clear();
                options.TokenHandlers.Add(new JwtValidationHandler(jwtOptions));
            });

        return builder;
    }

    public static WebApplicationBuilder AddAuthorization(this WebApplicationBuilder builder)
    {
        var requireAuthPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();

        builder.Services.AddAuthorizationBuilder()
            .SetDefaultPolicy(requireAuthPolicy);

        return builder;
    }

    public static IServiceCollection AddJwtOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.Configure<JwtOptions>(configuration.GetJwtOptionsSection());
    }

    public static IConfigurationSection GetJwtOptionsSection(this IConfiguration configuration)
    {
        return configuration.GetRequiredSection(JwtOptions.SectionName);
    }
}