using BookManagementWeb.Data;
using BookManagementWeb.Models.Entities;
using BookManagementWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementWeb.Controllers
{
    public class CTHoaDonController : Controller
    {
        private readonly BookstoreDbContext _context;
        public CTHoaDonController (BookstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Details(int maHoaDon)
        {
            List<int> ttMaSach = new List<int>();
            List<Sach> sachList = new List<Sach>();
            List<CTHoaDonViewModel> ctHDList = new List<CTHoaDonViewModel>();
            var hoaDon = _context.HOADONBANSACH.Where(x => x.MaHoaDon == maHoaDon).FirstOrDefault();
            var phieuThuTien = _context.PHIEUTHUTIEN.Where(x => x.MaHoaDon == maHoaDon).FirstOrDefault();
            var CTHDList = _context.CTHOADONBANSACH.Where(x => x.MaHoaDon == maHoaDon).ToList();

            int maKhachHang = _context.HOADONBANSACH
                .Where(x => x.MaHoaDon == maHoaDon)
                .Select(x => x.MaKhachHang)
                .FirstOrDefault();
            string tenKhachHang = _context.KHACHHANG.Where(x => x.MaKhachHang == maKhachHang).Select(x => x.HoVaTen).FirstOrDefault();

            foreach(var i in CTHDList)
            {
                ttMaSach.Add(i.MaSach);
            }

            int j = 0;
            foreach(var i in CTHDList)
            {
                CTHoaDonViewModel ctHD = new CTHoaDonViewModel();
                ctHD.MaHoaDon = hoaDon.MaHoaDon;
                ctHD.NgayLapHoaDon = hoaDon.NgayLapHoaDon;
                ctHD.MaKhachHang = maKhachHang;
                ctHD.TenKhachHang = tenKhachHang;
                ctHD.SoLuongBan = i.SoLuongBan;
                ctHD.DonGiaBan = i.DonGiaBan;
                ctHD.TongSoTienThu = phieuThuTien.TongSoTienThu;
                ctHD.SoTienKhachNo = phieuThuTien.SoTienKhachNo;
                ctHD.MaSach = ttMaSach[j];
                ctHD.TenSach = _context.SACH.Where(x => x.MaSach == ctHD.MaSach).Select(x => x.TenSach).FirstOrDefault();
                ctHD.TacGia = _context.SACH.Where(x => x.MaSach == ctHD.MaSach).Select(x => x.TacGia).FirstOrDefault();
                ctHD.TheLoai = _context.SACH.Where(x => x.MaSach == ctHD.MaSach).Select(x => x.TheLoai).FirstOrDefault();
                
                ctHDList.Add(ctHD);
                j += 1;
            }


            return View(ctHDList);
        }
    }
}
