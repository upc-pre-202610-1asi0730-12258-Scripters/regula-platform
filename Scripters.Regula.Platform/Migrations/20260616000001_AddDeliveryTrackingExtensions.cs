using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Scripters.Regula.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddDeliveryTrackingExtensions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "delivery_responsibles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_delivery_responsibles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "delivery_vehicles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    plate = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    brand = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_delivery_vehicles", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "delivery_responsibles",
                columns: new[] { "id", "created_at", "name", "updated_at" },
                values: new object[] { 1, null, "Responsable de prueba", null });

            migrationBuilder.InsertData(
                table: "delivery_vehicles",
                columns: new[] { "id", "brand", "created_at", "plate", "type", "updated_at" },
                values: new object[] { 1, "Toyota", null, "ABC-123", "Van", null });

            migrationBuilder.AddColumn<int>(
                name: "responsible_id",
                table: "deliveries",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "vehicle_id",
                table: "deliveries",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "deliveries",
                type: "longtext",
                nullable: false,
                defaultValue: "PENDING");

            migrationBuilder.AddColumn<int>(
                name: "item_count",
                table: "deliveries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "scheduled_time",
                table: "deliveries",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(2026, 6, 16, 9, 0, 0));

            migrationBuilder.AddColumn<string>(
                name: "delivered_at",
                table: "deliveries",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "deliveries",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "item_count", "responsible_id", "scheduled_time", "status", "vehicle_id" },
                values: new object[] { 5, 1, new DateTime(2026, 6, 16, 9, 0, 0), "PENDING", 1 });

            migrationBuilder.CreateIndex(
                name: "i_x_deliveries_responsible_id",
                table: "deliveries",
                column: "responsible_id");

            migrationBuilder.CreateIndex(
                name: "i_x_deliveries_vehicle_id",
                table: "deliveries",
                column: "vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "f_k_deliveries_delivery_responsibles_responsible_id",
                table: "deliveries",
                column: "responsible_id",
                principalTable: "delivery_responsibles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "f_k_deliveries_delivery_vehicles_vehicle_id",
                table: "deliveries",
                column: "vehicle_id",
                principalTable: "delivery_vehicles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_deliveries_delivery_responsibles_responsible_id",
                table: "deliveries");

            migrationBuilder.DropForeignKey(
                name: "f_k_deliveries_delivery_vehicles_vehicle_id",
                table: "deliveries");

            migrationBuilder.DropIndex(
                name: "i_x_deliveries_responsible_id",
                table: "deliveries");

            migrationBuilder.DropIndex(
                name: "i_x_deliveries_vehicle_id",
                table: "deliveries");

            migrationBuilder.DropColumn(name: "responsible_id", table: "deliveries");
            migrationBuilder.DropColumn(name: "vehicle_id", table: "deliveries");
            migrationBuilder.DropColumn(name: "status", table: "deliveries");
            migrationBuilder.DropColumn(name: "item_count", table: "deliveries");
            migrationBuilder.DropColumn(name: "scheduled_time", table: "deliveries");
            migrationBuilder.DropColumn(name: "delivered_at", table: "deliveries");

            migrationBuilder.DeleteData(table: "delivery_responsibles", keyColumn: "id", keyValue: 1);
            migrationBuilder.DeleteData(table: "delivery_vehicles", keyColumn: "id", keyValue: 1);

            migrationBuilder.DropTable(name: "delivery_vehicles");
            migrationBuilder.DropTable(name: "delivery_responsibles");
        }
    }
}
