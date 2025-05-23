﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aztamlider.Data.Migrations
{
    public partial class LoggerAddUsernameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Loggers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Loggers");
        }
    }
}
