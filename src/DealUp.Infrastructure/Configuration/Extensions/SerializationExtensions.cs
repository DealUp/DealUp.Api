using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace DealUp.Infrastructure.Configuration.Extensions;

public static class SerializationExtensions
{
    public static IMvcBuilder SetCamelCaseResponse(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }
}