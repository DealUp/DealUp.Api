using DealUp.Constants;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DealUp.Infrastructure.Extensions;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swaggerOptions =>
        {
            swaggerOptions.SwaggerDoc("v1", new OpenApiInfo { Title = ProjectConstants.Name, Version = "v1" });
            swaggerOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
            });
            swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Authorization",
                        In = ParameterLocation.Header
                    },
                    []
                }
            });
        });
    }
}