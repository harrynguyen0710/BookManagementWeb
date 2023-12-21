using BookManagementWeb.Data;
using BookManagementWeb.Models.Entities;
using BookManagementWeb.ViewModel;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookManagementWeb.Controllers
{
    public class PhieuNhapSachController : Controller
    {
        private readonly BookstoreDbContext _context;

        private string _currentDate;

        private PhieuNhapViewModel danhSachPN;

        private int randomMaPhieu;

        private readonly IWebHostEnvironment _webHost;
        public string CurrentDate()
        {
            _currentDate = DateTime.Now.ToShortDateString();
            return _currentDate;
        }

        public PhieuNhapSachController(BookstoreDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Index()
        {
            List<PhieuNhapSach>? phieuNhapSach;
            phieuNhapSach = _context.PHIEUNHAPSACH.ToList();

            List<CTPhieuNhapSach>? cTPhieuNhapSach;
            cTPhieuNhapSach = _context.CTPHIEUNHAPSACH.ToList();

            List<int> soLuong = new List<int>();

            foreach (var maPhieu in phieuNhapSach)
            {
                int count = 0;
                foreach (var cTMaPhieu in cTPhieuNhapSach)
                {
                    if (maPhieu.MaPhieuNhapSach == cTMaPhieu.MaPhieuNhapSach)
                    {
                        count += cTMaPhieu.SoLuongNhap;
                    }
                }
                soLuong.Add(count);
            }

            List<int> maPhieuNhap = phieuNhapSach.Select(item => item.MaPhieuNhapSach).ToList();
            List<DateTime> ngayLapPhieu = phieuNhapSach.Select(item => item.NgayLapPhieuNhap).ToList();

            PhieuNhapViewModel viewModel = new PhieuNhapViewModel()
            {
                maPhieuNhapSach = maPhieuNhap,
                ngayLapPhieuNhap = ngayLapPhieu,
                soLuongNhap = soLuong
            };
            _context.SaveChanges();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var sachList = _context.SACH.ToList();
            var sachViewModelList = sachList.Select(sach => new Sach
            {
                MaSach = sach.MaSach,
                TenSach = sach.TenSach
            }).ToList();

            ViewBag.SachList = new SelectList(sachViewModelList, "MaSach", "TenSach");

            return View();
        }

        [HttpPost]
        public IActionResult Create(PhieuNhapViewModel phieuNhapSach)
        {
            if (ModelState.IsValid)
            {
                // Lưu thông tin phiếu nhập sách
                PhieuNhapSach phieuNhap = new PhieuNhapSach
                {
                    NgayLapPhieuNhap = DateTime.Now
                };

                _context.PHIEUNHAPSACH.Add(phieuNhap);
                _context.SaveChanges();

                // Lưu thông tin chi tiết phiếu nhập sách
                var ctPhieuNhapList = phieuNhapSach.maSach
                    .Select((maSach, index) => new CTPhieuNhapSach
                    {
                        MaPhieuNhapSach = phieuNhap.MaPhieuNhapSach,
                        MaSach = maSach,
                        SoLuongNhap = phieuNhapSach.soLuongNhap[index],
                        DonGiaNhap = phieuNhapSach.donGiaNhap[index]
                    })
                    .ToList();

                _context.CTPHIEUNHAPSACH.AddRange(ctPhieuNhapList);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            // Nếu ModelState không hợp lệ, quay lại view với dữ liệu hiện tại
            // và sửa lại ViewBag.SachList nếu cần.
            var sachList = _context.SACH.ToList();
            var sachViewModelList = sachList.Select(sach => new Sach
            {
                MaSach = sach.MaSach,
                TenSach = sach.TenSach
            }).ToList();

            ViewBag.SachList = new SelectList(sachViewModelList, "MaSach", "TenSach");

            return View(phieuNhapSach);
        }


    }
}
