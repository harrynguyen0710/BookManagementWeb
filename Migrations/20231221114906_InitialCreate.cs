using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagementWeb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SachMaSach",
                table: "SACH",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SACH_SachMaSach",
                table: "SACH",
                column: "SachMaSach");

            migrationBuilder.AddForeignKey(
                name: "FK_SACH_SACH_SachMaSach",
                table: "SACH",
                column: "SachMaSach",
                principalTable: "SACH",
                principalColumn: "MaSach");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SACH_SACH_SachMaSach",
                table: "SACH");

            migrationBuilder.DropIndex(
                name: "IX_SACH_SachMaSach",
                table: "SACH");

            migrationBuilder.DropColumn(
                name: "SachMaSach",
                table: "SACH");
        }
    }
}
