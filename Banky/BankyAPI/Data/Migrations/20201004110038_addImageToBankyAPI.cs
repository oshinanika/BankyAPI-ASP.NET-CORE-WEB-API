using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankyAPI.Migrations
{
    public partial class addImageToBankyAPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "IdentificationImage",
                table: "Bank",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationImage",
                table: "Bank");
        }
    }
}
