using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Scripters.Regula.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddInventoryManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    owner_profile_id = table.Column<long>(type: "bigint", nullable: false),
                    inventory_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_inventories", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "gas_cylinder_stocks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    cylinder_type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    available = table.Column<int>(type: "int", nullable: false),
                    in_transit = table.Column<int>(type: "int", nullable: false),
                    observed = table.Column<int>(type: "int", nullable: false),
                    out_of_service = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    inventory_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_gas_cylinder_stocks", x => x.id);
                    table.ForeignKey(
                        name: "f_k_gas_cylinder_stocks_inventories_inventory_id",
                        column: x => x.inventory_id,
                        principalTable: "inventories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "movements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    movement_type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    cylinder_type = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    provider_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    profile_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    inventory_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_movements", x => x.id);
                    table.ForeignKey(
                        name: "f_k_movements_inventories_inventory_id",
                        column: x => x.inventory_id,
                        principalTable: "inventories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "company_movements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    destination = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    movement_reason = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false),
                    observation = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_movements", x => x.id);
                    table.ForeignKey(
                        name: "FK_company_movements_movements_id",
                        column: x => x.id,
                        principalTable: "movements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "distributor_movements",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    outbound_type = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_movements", x => x.id);
                    table.ForeignKey(
                        name: "FK_distributor_movements_movements_id",
                        column: x => x.id,
                        principalTable: "movements",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_gas_cylinder_stocks_inventory_id",
                table: "gas_cylinder_stocks",
                column: "inventory_id");

            migrationBuilder.CreateIndex(
                name: "i_x_inventories_owner_profile_id_inventory_type",
                table: "inventories",
                columns: new[] { "owner_profile_id", "inventory_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "i_x_movements_inventory_id",
                table: "movements",
                column: "inventory_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "company_movements");

            migrationBuilder.DropTable(
                name: "distributor_movements");

            migrationBuilder.DropTable(
                name: "gas_cylinder_stocks");

            migrationBuilder.DropTable(
                name: "movements");

            migrationBuilder.DropTable(
                name: "inventories");
        }
    }
}
