using DeliveryOrder.Api.Domain.Enums;
using DeliveryOrderEntity = DeliveryOrder.Api.Domain.Models.DeliveryOrder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryOrder.Api.Data.Configurations;

public sealed class DeliveryOrderConfiguration : IEntityTypeConfiguration<DeliveryOrderEntity>
{
    public void Configure(EntityTypeBuilder<DeliveryOrderEntity> builder)
    {
        builder.ToTable("delivery_orders");
        builder.HasKey(order => order.Id).HasName("pk_delivery_orders");
        builder.Property(order => order.Id).HasColumnName("id");
        builder.Property(order => order.OrderNumber).HasColumnName("order_number").HasMaxLength(32).IsRequired();
        builder.Property(order => order.SenderCity).HasColumnName("sender_city").HasMaxLength(100).IsRequired();
        builder.Property(order => order.SenderAddress).HasColumnName("sender_address").HasMaxLength(300).IsRequired();
        builder.Property(order => order.RecipientCity).HasColumnName("recipient_city").HasMaxLength(100).IsRequired();
        builder.Property(order => order.RecipientAddress).HasColumnName("recipient_address").HasMaxLength(300).IsRequired();
        builder.Property(order => order.CargoWeightKg).HasColumnName("cargo_weight_kg").HasPrecision(10, 3).IsRequired();
        builder.Property(order => order.PickupDate).HasColumnName("pickup_date").HasColumnType("date").IsRequired();
        builder.Property(order => order.Status).HasColumnName("status").HasMaxLength(32).HasConversion(
                                                                        status => status.ToString(),
                                                                        value => Enum.Parse<DeliveryOrderStatus>(value)).IsRequired();
        builder.Property(order => order.CreatedAtUtc).HasColumnName("created_at_utc").IsRequired();

        builder.HasIndex(order => order.OrderNumber).IsUnique().HasDatabaseName("ix_delivery_orders_order_number");
    }
}
