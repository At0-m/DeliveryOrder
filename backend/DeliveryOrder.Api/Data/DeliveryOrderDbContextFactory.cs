using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DeliveryOrder.Api.Data;

public sealed class DeliveryOrderDbContextFactory : IDesignTimeDbContextFactory<DeliveryOrderDbContext>
{
    public DeliveryOrderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DeliveryOrderDbContext>();
        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection") ?? "Host=localhost;Port=5432;Database=delivery_order;Username=delivery_order;Password=delivery_order";

        optionsBuilder.UseNpgsql(connectionString);

        return new DeliveryOrderDbContext(optionsBuilder.Options);
    }
}
