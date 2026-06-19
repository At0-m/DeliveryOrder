using DeliveryOrderEntity = DeliveryOrder.Api.Domain.Models.DeliveryOrder;
using Microsoft.EntityFrameworkCore;

namespace DeliveryOrder.Api.Data;

public sealed class DeliveryOrderDbContext(DbContextOptions<DeliveryOrderDbContext> options) : DbContext(options)
{
    public DbSet<DeliveryOrderEntity> DeliveryOrders => Set<DeliveryOrderEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeliveryOrderDbContext).Assembly);
    }
}
