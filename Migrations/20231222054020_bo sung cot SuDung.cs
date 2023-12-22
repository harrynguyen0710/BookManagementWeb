using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagementWeb.Migrations
{
    public partial class bosungcotSuDung : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ApDung",
                table: "THAYDOIQUYDINH",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApDung",
                table: "THAYDOIQUYDINH");
        }
    }
}
