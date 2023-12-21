using BookManagementWeb.Data;
using BookManagementWeb.Models.Entities;
using BookManagementWeb.ViewModel;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Linq;

namespace BookManagementWeb.Controllers
{
    public class PhieuNhapSachController : Controller
    {
        private readonly BookstoreDbContext _context;

        private PhieuNhapViewModel danhSachPN;

        private int randomMaPhieu;

        public PhieuNhapSachController(BookstoreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
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

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {


            // Lấy dữ liệu từ database hoặc nơi khác
            var sachList = _context.SACH.ToList();

            // Chuyển đổi danh sách sách sang SachViewModel
            var sachViewModelList = sachList.Select(sach => new Sach
            {
                MaSach = sach.MaSach,
                TenSach = sach.TenSach
            }).ToList();

            // Truyền danh sách sách ViewModel đến ViewBag hoặc ViewData
            ViewBag.SachList = new SelectList(sachViewModelList, "MaSach", "TenSach");

            List<PhieuNhapSach>? phieuNhapSach;
            phieuNhapSach = _context.PHIEUNHAPSACH.ToList();

            List<int> maPhieuNhap = phieuNhapSach.Select(item => item.MaPhieuNhapSach).ToList();

            PhieuNhapViewModel viewModel = new PhieuNhapViewModel()
            {
                maPhieuNhapSach = maPhieuNhap
            };

            Random random = new Random();
            randomMaPhieu = random.Next(1000, 9999);
            while (viewModel.maPhieuNhapSach.Contains(randomMaPhieu))
            {
                randomMaPhieu = random.Next(1000, 9999);
            }

            return View();
        }

        [HttpPost]
        public IActionResult Create(List<PhieuNhapViewModel> DSSach)
        {
            return Json(new { DSSach });

            /*if (DSSach == null)
            {
                return Json(new { message = "DSSach is null" });
            }
            else
            {
                return Json(new { message = "DSSach is not null" });
            }*/
            /*List<CTPhieuNhapSach> phieuNhapSach = new List<CTPhieuNhapSach>();
                for (int i = 0; i < DSSach.maSach.Count(); i++)
                {
                    CTPhieuNhapSach ctPhieuNhap = new CTPhieuNhapSach()
                    {
                        MaSach = DSSach.maSach[i],
                        MaPhieuNhapSach = randomMaPhieu,
                        SoLuongNhap = DSSach.soLuongNhap[i],
                        DonGiaNhap = DSSach.donGiaNhap[i]
                    };
                _context.Add(ctPhieuNhap);
                _context.SaveChanges();
            }
           

            List<CTPhieuNhapSach> danhSachPN;
            danhSachPN = _context.CTPHIEUNHAPSACH.ToList();

            PhieuNhapSach phieuNhap = new PhieuNhapSach()
            {
                MaPhieuNhapSach = randomMaPhieu,
                NgayLapPhieuNhap = DateTime.Now.Date
            };
            _context.Add(phieuNhap);
            _context.SaveChanges();

            List<Sach>? sach;
            sach = _context.SACH.ToList();

            foreach (Sach update in sach)
            {
                foreach (var i in danhSachPN)
                {
                    if (i.MaPhieuNhapSach == randomMaPhieu)
                    {
                        if (update.MaSach == i.MaSach)
                        {
                            update.SoLuongSach += i.SoLuongNhap;
                        }
                    }
                }
                _context.Update(update);
            }
            _context.SaveChanges();

            return RedirectToAction("Index");*/
        }
    }
}
