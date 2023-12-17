using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookManagementWeb.Migrations
{
    public partial class AddKhachHangAndSach : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KHACHHANG",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoVaTen = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SoTienNo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KHACHHANG", x => x.MaKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "SACH",
                columns: table => new
                {
                    MaSach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenSach = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TacGia = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    TheLoai = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SoLuongSach = table.Column<int>(type: "int", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SACH", x => x.MaSach);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KHACHHANG");

            migrationBuilder.DropTable(
                name: "SACH");
        }
    }
}
