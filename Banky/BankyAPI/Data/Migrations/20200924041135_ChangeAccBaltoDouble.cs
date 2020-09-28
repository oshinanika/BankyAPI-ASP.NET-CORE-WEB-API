using Microsoft.EntityFrameworkCore.Migrations;

namespace BankyAPI.Migrations
{
    public partial class ChangeAccBaltoDouble : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AccountBalance",
                table: "Bank",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccountBalance",
                table: "Bank",
                type: "int",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
