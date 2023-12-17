    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    #nullable disable

    namespace BookManagementWeb.Migrations
    {
        public partial class BoSungCacTableConlai : Migration
        {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.CreateTable(
                    name: "HOADONBANSACH",
                    columns: table => new
                    {
                        MaHoaDon = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        NgayLapHoaDon = table.Column<DateTime>(type: "date", nullable: false),
                        MaKhachHang = table.Column<int>(type: "int", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_HOADONBANSACH", x => x.MaHoaDon);
                        table.ForeignKey(
                            name: "FK_HOADONBANSACH_KHACHHANG_MaKhachHang",
                            column: x => x.MaKhachHang,
                            principalTable: "KHACHHANG",
                            principalColumn: "MaKhachHang",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateTable(
                    name: "PHIEUNHAPSACH",
                    columns: table => new
                    {
                        MaPhieuNhapSach = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        NgayLapPhieuNhap = table.Column<DateTime>(type: "date", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_PHIEUNHAPSACH", x => x.MaPhieuNhapSach);
                    });

                migrationBuilder.CreateTable(
                    name: "CTHOADONBANSACH",
                    columns: table => new
                    {
                        MaHoaDon = table.Column<int>(type: "int", nullable: false),
                        MaSach = table.Column<int>(type: "int", nullable: false),
                        SoLuongBan = table.Column<int>(type: "int", nullable: false),
                        DonGiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_CTHOADONBANSACH", x => new { x.MaHoaDon, x.MaSach });
                        table.ForeignKey(
                            name: "FK_CTHOADONBANSACH_HOADONBANSACH_MaHoaDon",
                            column: x => x.MaHoaDon,
                            principalTable: "HOADONBANSACH",
                            principalColumn: "MaHoaDon",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_CTHOADONBANSACH_SACH_MaSach",
                            column: x => x.MaSach,
                            principalTable: "SACH",
                            principalColumn: "MaSach",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateTable(
                    name: "PHIEUTHUTIEN",
                    columns: table => new
                    {
                        MaPhieuThuTien = table.Column<int>(type: "int", nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        MaHoaDon = table.Column<int>(type: "int", nullable: false),
                        TongSoTienThu = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                        NgayLapPhieuThuTien = table.Column<DateTime>(type: "date", nullable: false),
                        SoTienKhachNo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_PHIEUTHUTIEN", x => x.MaPhieuThuTien);
                        table.ForeignKey(
                            name: "FK_PHIEUTHUTIEN_HOADONBANSACH_MaHoaDon",
                            column: x => x.MaHoaDon,
                            principalTable: "HOADONBANSACH",
                            principalColumn: "MaHoaDon",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateTable(
                    name: "CTPHIEUNHAPSACH",
                    columns: table => new
                    {
                        MaPhieuNhapSach = table.Column<int>(type: "int", nullable: false),
                        MaSach = table.Column<int>(type: "int", nullable: false),
                        SoLuongNhap = table.Column<int>(type: "int", nullable: false),
                        DonGiaNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_CTPHIEUNHAPSACH", x => new { x.MaPhieuNhapSach, x.MaSach });
                        table.ForeignKey(
                            name: "FK_CTPHIEUNHAPSACH_PHIEUNHAPSACH_MaPhieuNhapSach",
                            column: x => x.MaPhieuNhapSach,
                            principalTable: "PHIEUNHAPSACH",
                            principalColumn: "MaPhieuNhapSach",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_CTPHIEUNHAPSACH_SACH_MaSach",
                            column: x => x.MaSach,
                            principalTable: "SACH",
                            principalColumn: "MaSach",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_CTHOADONBANSACH_MaSach",
                    table: "CTHOADONBANSACH",
                    column: "MaSach");

                migrationBuilder.CreateIndex(
                    name: "IX_CTPHIEUNHAPSACH_MaSach",
                    table: "CTPHIEUNHAPSACH",
                    column: "MaSach");

                migrationBuilder.CreateIndex(
                    name: "IX_HOADONBANSACH_MaKhachHang",
                    table: "HOADONBANSACH",
                    column: "MaKhachHang");

                migrationBuilder.CreateIndex(
                    name: "IX_PHIEUTHUTIEN_MaHoaDon",
                    table: "PHIEUTHUTIEN",
                    column: "MaHoaDon");
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.DropTable(
                    name: "CTHOADONBANSACH");

                migrationBuilder.DropTable(
                    name: "CTPHIEUNHAPSACH");

                migrationBuilder.DropTable(
                    name: "PHIEUTHUTIEN");

                migrationBuilder.DropTable(
                    name: "PHIEUNHAPSACH");

                migrationBuilder.DropTable(
                    name: "HOADONBANSACH");
            }
        }
    }
