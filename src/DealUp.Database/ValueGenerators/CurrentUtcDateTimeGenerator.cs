using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DealUp.Database.ValueGenerators;

public class CurrentUtcDateTimeGenerator : ValueGenerator<DateTime>
{
    public override bool GeneratesTemporaryValues => false;

    public override DateTime Next(EntityEntry entry)
    {
        return DateTime.UtcNow;
    }
}