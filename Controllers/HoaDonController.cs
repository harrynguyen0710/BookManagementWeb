using BookManagementWeb.Data;
using BookManagementWeb.Models.Entities;
using BookManagementWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace BookManagementWeb.Controllers
{
    public class HoaDonController : Controller
    {
        private readonly BookstoreDbContext _context;

        public HoaDonController(BookstoreDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            var hoaDonList = _context.HOADONBANSACH
                .Select(hd => new KhachHangViewModel
                {
                    MaHoaDon = hd.MaHoaDon,
                    NgayLapHoaDon = hd.NgayLapHoaDon,
                    MaKhachHang = hd.MaKhachHang,
                    TenKhachHang = _context.KHACHHANG
                        .Where(kh => kh.MaKhachHang == hd.MaKhachHang)
                        .Select(kh => kh.HoVaTen)
                        .FirstOrDefault()
                })
                .ToList();

            return View(hoaDonList);
        }

        public IActionResult Create()
        {
            ViewBag.MaSachList = new SelectList(_context.SACH, "MaSach", "TenSach");
            ViewBag.MaKhachHangList = new SelectList(_context.KHACHHANG, "MaKhachHang", "HoVaTen");

            var hoaDonViewModel = new HoaDonViewModel();
            return View(hoaDonViewModel);
        }

        [HttpPost]
        public IActionResult Create(HoaDonViewModel hdVM, string HDSachList)
        {

            decimal giaTriDonHang = 0;
            hdVM.HDSachList = JsonConvert.DeserializeObject<List<HoaDonViewModel>>(HDSachList);
            var phanTuCuoi = hdVM.HDSachList.Count - 1;
            HoaDon hd = new HoaDon();
            hd.MaHoaDon = hdVM.HDSachList[0].MaHoaDon;
            hd.MaKhachHang = hdVM.HDSachList[0].MaKhachHang;
            hd.NgayLapHoaDon = hdVM.HDSachList[phanTuCuoi].NgayLapHoaDon;

            _context.HOADONBANSACH.Add(hd);
            _context.SaveChanges();

            foreach (var sachBan in hdVM.HDSachList)
            {
                CTHoaDon chiTietHoadon = new CTHoaDon();
                chiTietHoadon.MaHoaDon = hdVM.MaHoaDon;
                chiTietHoadon.MaSach = sachBan.MaSach;
                chiTietHoadon.SoLuongBan = sachBan.SoLuongBan;
                chiTietHoadon.HoaDon = hd;
                chiTietHoadon.Sach = _context.SACH.Where(x => x.MaSach == sachBan.MaSach).FirstOrDefault();


                chiTietHoadon.DonGiaBan = sachBan.DonGiaBan;

                giaTriDonHang += (chiTietHoadon.SoLuongBan * chiTietHoadon.DonGiaBan);
                _context.CTHOADONBANSACH.Add(chiTietHoadon);

                var sach = _context.SACH.SingleOrDefault(x => x.MaSach == sachBan.MaSach);
                if (sach != null)
                {
                    sach.SoLuongSach -= sachBan.SoLuongBan;
                }
            }

            _context.SaveChanges();

            TempData["MaHoaDon"] = hd.MaHoaDon;
            TempData["MaKhachHang"] = hdVM.HDSachList[0].MaKhachHang;
            TempData["TenKhachHang"] = LayTenKhachHangTheoMa(hdVM.HDSachList[0].MaKhachHang);
            TempData["NgayLapHoaDon"] = hdVM.HDSachList[0].NgayLapHoaDon;
            TempData["TongGiaTriHoaDon"] = (giaTriDonHang).ToString();
            TempData["TienNo"] = LayTienNoCuaKhach(hdVM.HDSachList[0].MaKhachHang).ToString();
            return RedirectToAction("Create", "PhieuThuTien");
        }


        public decimal LayTienNoCuaKhach(int maKhachHang)
        {
            var tienNo = _context.KHACHHANG.Where(x => x.MaKhachHang == maKhachHang)
                                .Select(x => x.SoTienNo).FirstOrDefault();
            return tienNo;
        }

        public string LayTenKhachHangTheoMa(int maKhachHang)
        {
            var tenKhachHang = _context.KHACHHANG.Where(x => x.MaKhachHang == maKhachHang)
                                    .Select(x => x.HoVaTen).FirstOrDefault();
            return tenKhachHang;

        }


        [HttpGet]
        public IActionResult LaySoLuongTonTrongKho(int maSach)
        {
            var soLuongTrongKho = _context.SACH
                .Where(s => s.MaSach == maSach)
                    .Select(s => s.SoLuongSach).FirstOrDefault();

            return Json(new { soLuongTrongKho });
        }




    }
}
