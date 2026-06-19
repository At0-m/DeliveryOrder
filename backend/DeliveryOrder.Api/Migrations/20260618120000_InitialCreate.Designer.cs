using System;
using DeliveryOrder.Api.Data;
using DeliveryOrder.Api.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeliveryOrder.Api.Migrations;

[DbContext(typeof(DeliveryOrderDbContext))]
[Migration("20260618120000_InitialCreate")]
partial class InitialCreate
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "9.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 63);

        NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

        modelBuilder.Entity("DeliveryOrder.Api.Domain.Models.DeliveryOrder", builder =>
        {
            builder.Property<long>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("bigint")
                .HasColumnName("id")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            builder.Property<decimal>("CargoWeightKg")
                .HasPrecision(10, 3)
                .HasColumnType("numeric(10,3)")
                .HasColumnName("cargo_weight_kg");

            builder.Property<DateTime>("CreatedAtUtc")
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at_utc");

            builder.Property<string>("OrderNumber")
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnType("character varying(32)")
                .HasColumnName("order_number");

            builder.Property<DateOnly>("PickupDate")
                .HasColumnType("date")
                .HasColumnName("pickup_date");

            builder.Property<string>("RecipientAddress")
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnType("character varying(300)")
                .HasColumnName("recipient_address");

            builder.Property<string>("RecipientCity")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("character varying(100)")
                .HasColumnName("recipient_city");

            builder.Property<string>("SenderAddress")
                .IsRequired()
                .HasMaxLength(300)
                .HasColumnType("character varying(300)")
                .HasColumnName("sender_address");

            builder.Property<string>("SenderCity")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("character varying(100)")
                .HasColumnName("sender_city");

            builder.Property<DeliveryOrderStatus>("Status")
                .HasConversion<string>()
                .HasMaxLength(32)
                .HasColumnType("character varying(32)")
                .HasColumnName("status");

            builder.HasKey("Id")
                .HasName("pk_delivery_orders");

            builder.HasIndex("OrderNumber")
                .IsUnique()
                .HasDatabaseName("ix_delivery_orders_order_number");

            builder.ToTable("delivery_orders");
        });
#pragma warning restore 612, 618
    }
}
