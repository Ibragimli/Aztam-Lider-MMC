using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztamlider.Data.Migrations
{
    public partial class ServiceNameAd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "ServiceTypes",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ServiceNameId",
                table: "References",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ServiceName",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAz = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NameEn = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceName", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_References_ServiceNameId",
                table: "References",
                column: "ServiceNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_References_ServiceName_ServiceNameId",
                table: "References",
                column: "ServiceNameId",
                principalTable: "ServiceName",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_References_ServiceName_ServiceNameId",
                table: "References");

            migrationBuilder.DropTable(
                name: "ServiceName");

            migrationBuilder.DropIndex(
                name: "IX_References_ServiceNameId",
                table: "References");

            migrationBuilder.DropColumn(
                name: "ServiceNameId",
                table: "References");

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "ServiceTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);
        }
    }
}
