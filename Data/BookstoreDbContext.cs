using Microsoft.EntityFrameworkCore;
using BookManagementWeb.Models.Entities;
namespace BookManagementWeb.Data
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext() { }
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
            : base(options) { }

        public virtual DbSet<Sach> SACH { get; set; }
        public virtual DbSet<KhachHang> KHACHHANG { get; set; }
/*        public virtual DbSet<PhieuNhapSach> PhieuNhapSachCollection { get; set; }
        public virtual DbSet<CTPhieuNhapSach> CTPhieuNhapSachCollection { get; set; }
        public virtual DbSet<HoaDon> HoaDonCollection { get; set; }
        public virtual DbSet<CTHoaDon> CTHoaDonCollection { get; set; }*/
        public virtual DbSet<PhieuThuTien> PHIEUTHUTIEN { get; set; }
    }
}
