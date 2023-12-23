using BookManagementWeb.Data;
using BookManagementWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementWeb.Controllers
{
    public class PhieuThuTienController : Controller
    {
        private readonly BookstoreDbContext _context;

        public PhieuThuTienController(BookstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<PhieuThuTien> phieuThuTienList = _context.PHIEUTHUTIEN.ToList();
            return View(phieuThuTienList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var maHoaDon = TempData["MaHoaDon"];
            var maKhachHang = TempData["MaKhachHang"];
            var ngayLapHoaDon = TempData["NgayLapHoaDon"];
            var tongGiaTriHoaDon = TempData["TongGiaTriHoaDon"];
            var tenKhachHang = TempData["TenKhachHang"];
            var soTienKhachNo = TempData["TienNo"];

            ViewBag.MaHoaDon = maHoaDon;
            ViewBag.MaKhachHang = maKhachHang;
            ViewBag.NgayLapHoaDon = ngayLapHoaDon;
            ViewBag.TongGiaTriHoaDon = tongGiaTriHoaDon;
            ViewBag.TenKhachHang = tenKhachHang;
            ViewBag.TienNo = soTienKhachNo;

            string ngayTaoHoaDon = ngayLapHoaDon.ToString();

            HoaDon hoaDon = new HoaDon
            {
                MaHoaDon = Convert.ToInt32(maHoaDon),
                NgayLapHoaDon = DateTime.Parse(ngayTaoHoaDon),
                MaKhachHang = Convert.ToInt32(maKhachHang)
            };
            TempData["MaHoaDon"] = hoaDon.MaHoaDon;
            TempData["NgayLapHoaDon"] = hoaDon.NgayLapHoaDon;
            TempData["MaKhachHang"] = hoaDon.MaKhachHang;

            return View();
        }

        [HttpPost]
        public IActionResult Create(PhieuThuTien phieuThuTien)
        {
            int maHoaDon = Convert.ToInt32(TempData["MaHoaDon"]);
            DateTime ngayLapHoaDon = Convert.ToDateTime(TempData["NgayLapHoaDon"]);
            int maKhachHang = Convert.ToInt32(TempData["MaKhachHang"]);

            HoaDon hoaDon = new HoaDon
            {
                MaHoaDon = maHoaDon,
                NgayLapHoaDon = ngayLapHoaDon,
                MaKhachHang = maKhachHang
            };

            decimal soTienKhachNo = _context.KHACHHANG.Where(x => x.MaKhachHang == hoaDon.MaKhachHang)
                                                            .Select(x => x.SoTienNo).FirstOrDefault();

            var tongGiaTriHoaDonTempData = TempData["TongGiaTriHoaDon"]?.ToString();
            /*            decimal tongGiaTriHoaDon = string.IsNullOrEmpty(tongGiaTriHoaDonTempData) ? 0 : decimal.Parse(tongGiaTriHoaDonTempData);
            */
            decimal tongGiaTriHoaDon = 0;
            

            var khachHang = _context.KHACHHANG.Where(x => x.MaKhachHang == hoaDon.MaKhachHang).FirstOrDefault();

            phieuThuTien.SoTienKhachNo = soTienKhachNo;
            phieuThuTien.MaHoaDon = hoaDon.MaHoaDon;

            List<CTHoaDon> ctHoaDonList = new List<CTHoaDon>();
            ctHoaDonList = _context.CTHOADONBANSACH.Where(x => x.MaHoaDon == hoaDon.MaHoaDon).ToList();
            foreach(var i in ctHoaDonList)
            {
                tongGiaTriHoaDon += (i.DonGiaBan * i.SoLuongBan);
            }


            decimal soTienNoMoi = phieuThuTien.TongSoTienThu - (phieuThuTien.SoTienKhachNo + tongGiaTriHoaDon);
            if(soTienNoMoi >= 0)
            {
                khachHang.SoTienNo = 0;
            }
            else
            {
                khachHang.SoTienNo -= soTienNoMoi;
            }

            _context.PHIEUTHUTIEN.Add(phieuThuTien);
            _context.SaveChanges();

            return RedirectToAction("Index","HoaDon");

        }


    }
}
