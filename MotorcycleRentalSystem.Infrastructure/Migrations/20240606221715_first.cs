using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MotorcycleRentalSystem.Infrastructure.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_motorcycle",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    year = table.Column<long>(type: "bigint", nullable: false),
                    model = table.Column<string>(type: "text", nullable: false),
                    license_plate = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_motorcycle", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_user",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    full_name = table.Column<string>(type: "text", nullable: false),
                    phone_number = table.Column<string>(type: "text", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_current_year_motorcycle",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_by = table.Column<long>(type: "bigint", nullable: false),
                    motorcycle_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_current_year_motorcycle", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_current_year_motorcycle_tb_motorcycle_motorcycle_id",
                        column: x => x.motorcycle_id,
                        principalTable: "tb_motorcycle",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_current_year_motorcycle_tb_user_created_by",
                        column: x => x.created_by,
                        principalTable: "tb_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "tb_user",
                columns: new[] { "id", "active", "created_at", "email", "full_name", "password", "phone_number", "type", "updated_at" },
                values: new object[,]
                {
                    { 1L, true, new DateTime(2024, 6, 6, 22, 17, 15, 449, DateTimeKind.Utc).AddTicks(5900), "system@posts.com", "Administrator", "1234", "31 99999-9999", "System", null },
                    { 2L, true, new DateTime(2024, 6, 6, 22, 17, 15, 449, DateTimeKind.Utc).AddTicks(5920), "admin@posts.com", "Administrator", "1234", "31 99999-9999", "Administrator", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_current_year_motorcycle_created_by",
                table: "tb_current_year_motorcycle",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_tb_current_year_motorcycle_motorcycle_id",
                table: "tb_current_year_motorcycle",
                column: "motorcycle_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_motorcycle_license_plate",
                table: "tb_motorcycle",
                column: "license_plate",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_current_year_motorcycle");

            migrationBuilder.DropTable(
                name: "tb_motorcycle");

            migrationBuilder.DropTable(
                name: "tb_user");
        }
    }
}
