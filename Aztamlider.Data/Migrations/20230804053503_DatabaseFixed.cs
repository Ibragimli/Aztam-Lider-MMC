using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztamlider.Data.Migrations
{
    public partial class DatabaseFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferenceImages_References_ReferenceId",
                table: "ReferenceImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ReferenceImages_Services_ServiceId",
                table: "ReferenceImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceImages_References_ReferenceId",
                table: "ServiceImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceImages_Services_ServiceId",
                table: "ServiceImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceTypes_ServiceTypes_ServiceTypeId",
                table: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_ServiceTypes_ServiceTypeId",
                table: "ServiceTypes");

            migrationBuilder.DropIndex(
                name: "IX_ServiceImages_ReferenceId",
                table: "ServiceImages");

            migrationBuilder.DropIndex(
                name: "IX_ReferenceImages_ServiceId",
                table: "ReferenceImages");

            migrationBuilder.DropColumn(
                name: "ServiceTypeId",
                table: "ServiceTypes");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ReferenceImages");

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPoster",
                table: "ServiceImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "References",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "BuildingType",
                table: "References",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)",
                oldMaxLength: 120);

            migrationBuilder.AlterColumn<int>(
                name: "ReferenceId",
                table: "ReferenceImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPoster",
                table: "ReferenceImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ReferenceImages_References_ReferenceId",
                table: "ReferenceImages",
                column: "ReferenceId",
                principalTable: "References",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceImages_Services_ServiceId",
                table: "ServiceImages",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReferenceImages_References_ReferenceId",
                table: "ReferenceImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceImages_Services_ServiceId",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "IsPoster",
                table: "ServiceImages");

            migrationBuilder.DropColumn(
                name: "IsPoster",
                table: "ReferenceImages");

            migrationBuilder.AddColumn<int>(
                name: "ServiceTypeId",
                table: "ServiceTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ServiceId",
                table: "ServiceImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ReferenceId",
                table: "ServiceImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "References",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "BuildingType",
                table: "References",
                type: "nvarchar(120)",
                maxLength: 120,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "ReferenceId",
                table: "ReferenceImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "ReferenceImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceTypes_ServiceTypeId",
                table: "ServiceTypes",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceImages_ReferenceId",
                table: "ServiceImages",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_ReferenceImages_ServiceId",
                table: "ReferenceImages",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferenceImages_References_ReferenceId",
                table: "ReferenceImages",
                column: "ReferenceId",
                principalTable: "References",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReferenceImages_Services_ServiceId",
                table: "ReferenceImages",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceImages_References_ReferenceId",
                table: "ServiceImages",
                column: "ReferenceId",
                principalTable: "References",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceImages_Services_ServiceId",
                table: "ServiceImages",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceTypes_ServiceTypes_ServiceTypeId",
                table: "ServiceTypes",
                column: "ServiceTypeId",
                principalTable: "ServiceTypes",
                principalColumn: "Id");
        }
    }
}
