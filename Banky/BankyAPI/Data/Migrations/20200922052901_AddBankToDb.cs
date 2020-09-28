using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankyAPI.Migrations
{
    public partial class AddBankToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountHolderName = table.Column<string>(nullable: false),
                    AccountNumber = table.Column<string>(nullable: false),
                    AccountBalance = table.Column<int>(nullable: false),
                    AccountCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank");
        }
    }
}
