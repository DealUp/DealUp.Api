using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DealUp.Database.Extensions;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<TProperty> HasJsonConversion<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
                value => JsonSerializer.Serialize(value, JsonSerializerOptions.Default),
                value => JsonSerializer.Deserialize<TProperty>(value, JsonSerializerOptions.Default)!)
            .IsUnicode();
    }
}