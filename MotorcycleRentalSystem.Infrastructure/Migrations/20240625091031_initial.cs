using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MotorcycleRentalSystem.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_rider",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DriverLicenseNumber = table.Column<string>(type: "text", nullable: false),
                    DriverLicenseType = table.Column<string>(type: "text", nullable: false),
                    DriverLicenseImageKey = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_rider", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "tb_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "email", "full_name" },
                values: new object[] { new DateTime(2024, 6, 25, 9, 10, 30, 797, DateTimeKind.Utc).AddTicks(6879), "system@motorcyclerentalsystem.com", "System" });

            migrationBuilder.UpdateData(
                table: "tb_user",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "email" },
                values: new object[] { new DateTime(2024, 6, 25, 9, 10, 30, 797, DateTimeKind.Utc).AddTicks(6880), "admin@motorcyclerentalsystem.com" });

            migrationBuilder.CreateIndex(
                name: "IX_tb_rider_Cnpj",
                table: "tb_rider",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_rider_DriverLicenseNumber",
                table: "tb_rider",
                column: "DriverLicenseNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_rider");

            migrationBuilder.UpdateData(
                table: "tb_user",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_at", "email", "full_name" },
                values: new object[] { new DateTime(2024, 6, 6, 22, 17, 15, 449, DateTimeKind.Utc).AddTicks(5900), "system@posts.com", "Administrator" });

            migrationBuilder.UpdateData(
                table: "tb_user",
                keyColumn: "id",
                keyValue: 2L,
                columns: new[] { "created_at", "email" },
                values: new object[] { new DateTime(2024, 6, 6, 22, 17, 15, 449, DateTimeKind.Utc).AddTicks(5920), "admin@posts.com" });
        }
    }
}
