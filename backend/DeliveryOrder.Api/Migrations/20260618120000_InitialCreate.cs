using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DeliveryOrder.Api.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "delivery_orders",
            columns: table => new
            {
                id = table.Column<long>(type: "bigint", nullable: false).Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                order_number = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                sender_city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                sender_address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                recipient_city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                recipient_address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                cargo_weight_kg = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                pickup_date = table.Column<DateOnly>(type: "date", nullable: false),
                status = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_delivery_orders", x => x.id);
            });

        migrationBuilder.CreateIndex(
            name: "ix_delivery_orders_order_number",
            table: "delivery_orders",
            column: "order_number",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "delivery_orders");
    }
}
