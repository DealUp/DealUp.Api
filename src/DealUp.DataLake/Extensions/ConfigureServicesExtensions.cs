using DealUp.DataLake.Configuration;
using DealUp.DataLake.Interfaces;
using DealUp.DataLake.Models;
using DealUp.Utils;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DealUp.DataLake.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddDataLake(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection
            .Configure<DataLakeOptions>(configuration.GetSection(DataLakeOptions.SectionName))
            .AddScoped<IDataLakeFactory, DataLakeFactory>()
            .AddScoped<IDataLake>(serviceProvider =>
            {
                var dataLakeFactory = serviceProvider.GetRequiredService<IDataLakeFactory>();
                var options = serviceProvider.GetRequiredService<IOptions<DataLakeOptions>>();
                return dataLakeFactory.CreateDataLake(options.Value.Mode.ToEnum<DataLakeType>());
            });
    }
}