using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagementWeb.Migrations
{
    public partial class Bosungquydinh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "THAYDOIQUYDINH",
                columns: table => new
                {
                    LanThayDoi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinTonTruocNhap = table.Column<int>(type: "int", nullable: false),
                    MinTonSauBan = table.Column<int>(type: "int", nullable: false),
                    MaxNo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinNhap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THAYDOIQUYDINH", x => x.LanThayDoi);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "THAYDOIQUYDINH");

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
    }
}
