using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.Migrations
{
    public partial class MigrationParkingTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceTables",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartSurveillance = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndSurveillance = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ItialHourlyRate = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    FinalHourlyRate = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceTables", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VehicleRegistrations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LicensePlate = table.Column<string>(type: "TEXT", nullable: false),
                    Enter = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Exit = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AmountCharged = table.Column<decimal>(type: "decimal(18, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleRegistrations", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceTables");

            migrationBuilder.DropTable(
                name: "VehicleRegistrations");
        }
    }
}
