using DealUp.Infrastructure.Configuration;
using DealUp.Infrastructure.Handlers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DealUp.Infrastructure.Extensions;

public static class AuthExtensions
{
    public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddJwtOptions(builder.Configuration)
            .AddOauthOptions(builder.Configuration)
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddGoogle(options =>
            {
                var googleOptions = builder.Configuration.GetOauthOptionsSection().Get<OauthOptions>()!.GoogleProviderOptions;

                options.ClientId = googleOptions.ClientId;
                options.ClientSecret = googleOptions.ClientSecret;
                options.CallbackPath = googleOptions.CallbackUrl;
                options.SaveTokens = false;
            })
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

    public static IServiceCollection AddOauthOptions(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.Configure<OauthOptions>(configuration.GetOauthOptionsSection());
    }

    public static IConfigurationSection GetJwtOptionsSection(this IConfiguration configuration)
    {
        return configuration.GetRequiredSection(JwtOptions.SectionName);
    }

    public static IConfigurationSection GetOauthOptionsSection(this IConfiguration configuration)
    {
        return configuration.GetRequiredSection(OauthOptions.SectionName);
    }
}