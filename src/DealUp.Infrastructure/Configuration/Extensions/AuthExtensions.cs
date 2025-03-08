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
                var jwtOptions = builder.Configuration.GetRequiredSection(JwtOptions.SectionName).Get<JwtOptions>()!;

                options.RequireHttpsMetadata = false;
                options.TokenHandlers.Clear();
                options.TokenHandlers.Add(new JwtValidationHandler(jwtOptions));
            });

        return builder;
    }

    private static IServiceCollection AddJwtOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
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
}