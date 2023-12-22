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
        public virtual DbSet<PhieuNhapSach> PHIEUNHAPSACH { get; set; }
        public virtual DbSet<CTPhieuNhapSach> CTPHIEUNHAPSACH { get; set; }
        public virtual DbSet<HoaDon> HOADONBANSACH { get; set; }
        public virtual DbSet<CTHoaDon> CTHOADONBANSACH { get; set; }
        public virtual DbSet<PhieuThuTien> PHIEUTHUTIEN { get; set; }
        public virtual DbSet<ThayDoiQuyDinh> THAYDOIQUYDINH { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhieuThuTien>()
                .HasKey(ptt => ptt.MaPhieuThuTien);

            modelBuilder.Entity<PhieuThuTien>()
                .HasOne(ptt => ptt.HoaDon)
                .WithMany(i => i.PhieuThuTienCollection)
                .HasForeignKey(ptt => ptt.MaHoaDon);

            modelBuilder.Entity<HoaDon>()
                .HasKey(hd => hd.MaHoaDon);

            modelBuilder.Entity<KhachHang>()
                .HasMany(kh => kh.HoaDonCollectionKH)
                .WithOne(hd => hd.KhachHang)
                .HasForeignKey(hd => hd.MaKhachHang);

            modelBuilder.Entity<CTHoaDon>()
                .HasKey(id => new { id.MaHoaDon, id.MaSach });

            modelBuilder.Entity<CTHoaDon>()
                .HasOne(id => id.HoaDon)
                .WithMany(i => i.CTHoaDonCollectionHD)
                .HasForeignKey(id => id.MaHoaDon);

            modelBuilder.Entity<CTHoaDon>()
                .HasOne(id => id.Sach)
                .WithMany(b => b.CTHoaDonCollectionSach)
                .HasForeignKey(id => id.MaSach);

            modelBuilder.Entity<CTPhieuNhapSach>()
                .HasKey(ct => new { ct.MaPhieuNhapSach, ct.MaSach });

            modelBuilder.Entity<CTPhieuNhapSach>()
                .HasOne(ct => ct.Sach)
                .WithMany(b => b.CTPhieuNhapSachCollectionSach)
                .HasForeignKey(ct => ct.MaSach);

            modelBuilder.Entity<CTPhieuNhapSach>()
                .HasOne(ct => ct.PhieuNhapSach)
                .WithMany(pns => pns.CTPhieuNhapSachPN)
                .HasForeignKey(ct => ct.MaPhieuNhapSach);
        }
    }
}
