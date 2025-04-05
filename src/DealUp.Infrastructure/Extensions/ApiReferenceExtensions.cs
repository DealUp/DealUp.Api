using DealUp.Constants;
using DealUp.Infrastructure.OpenApiTransformers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;

namespace DealUp.Infrastructure.Extensions;

public static class ApiReferenceExtensions
{
    public static IServiceCollection AddApiReference(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
        });
    }

    public static void UseApiReferenceIfDevelopment(this WebApplication application)
    {
        if (application.Environment.IsDevelopment())
        {
            application.MapOpenApi();
            application.MapScalarApiReference("/docs/api", options =>
            {
                options
                    .WithTitle($"{ProjectConstants.Name} API Reference")
                    .WithClientButton(false)
                    .WithDownloadButton(false)
                    .WithPreferredScheme(JwtBearerDefaults.AuthenticationScheme);
            });
        }
    }
}