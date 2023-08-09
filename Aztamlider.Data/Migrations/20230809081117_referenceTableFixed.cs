using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztamlider.Data.Migrations
{
    public partial class referenceTableFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time",
                table: "Loggers");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "References",
                newName: "LocationEn");

            migrationBuilder.RenameColumn(
                name: "BuildingType",
                table: "References",
                newName: "BuildingTypeEn");

            migrationBuilder.AddColumn<string>(
                name: "BuildingTypeAz",
                table: "References",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LocationAz",
                table: "References",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Product",
                table: "Loggers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuildingTypeAz",
                table: "References");

            migrationBuilder.DropColumn(
                name: "LocationAz",
                table: "References");

            migrationBuilder.DropColumn(
                name: "Product",
                table: "Loggers");

            migrationBuilder.RenameColumn(
                name: "LocationEn",
                table: "References",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "BuildingTypeEn",
                table: "References",
                newName: "BuildingType");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Loggers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
