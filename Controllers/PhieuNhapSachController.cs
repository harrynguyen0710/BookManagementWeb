using BookManagementWeb.Data;
using BookManagementWeb.Models.Entities;
using BookManagementWeb.ViewModel;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BookManagementWeb.Controllers
{
    public class PhieuNhapSachController : Controller
    {
        private readonly BookstoreDbContext _context;

        public PhieuNhapSachController(BookstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var phieuNhapSachList = _context.PHIEUNHAPSACH.ToList();
            return View(phieuNhapSachList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaSachList = new SelectList(_context.SACH, "MaSach", "TenSach");
            var phieuNhapViewModel = new PhieuNhapViewModel();
            phieuNhapViewModel.NgayLapPhieuNhap = DateTime.Now.Date;
            return View(phieuNhapViewModel);
        }

        [HttpPost]
        public IActionResult Create(PhieuNhapViewModel pnVM, string PNSachList)
        {
            pnVM.phieuNhapViewModelList = JsonConvert.DeserializeObject<List<PhieuNhapViewModel>>(PNSachList);


            var phanTuCuoi = pnVM.phieuNhapViewModelList.Count - 1;
            PhieuNhapSach phieuNhapSach = new PhieuNhapSach();
            phieuNhapSach.MaPhieuNhapSach = pnVM.phieuNhapViewModelList[0].MaPhieuNhapSach;
            phieuNhapSach.NgayLapPhieuNhap = pnVM.phieuNhapViewModelList[phanTuCuoi].NgayLapPhieuNhap;

            _context.PHIEUNHAPSACH.Add(phieuNhapSach);
            _context.SaveChanges();

            foreach (var sachNhap in pnVM.phieuNhapViewModelList)
            {
                CTPhieuNhapSach ctPhieuNhapSach = new CTPhieuNhapSach();

                ctPhieuNhapSach.MaPhieuNhapSach = phieuNhapSach.MaPhieuNhapSach;
                ctPhieuNhapSach.MaSach = sachNhap.MaSach;
                ctPhieuNhapSach.SoLuongNhap = sachNhap.SoLuongNhap;
                ctPhieuNhapSach.DonGiaNhap = sachNhap.DonGiaNhap;

                _context.CTPHIEUNHAPSACH.Add(ctPhieuNhapSach);
                var sach = _context.SACH.SingleOrDefault(x => x.MaSach == sachNhap.MaSach);
                if (sach != null)
                {
                    sach.SoLuongSach += sachNhap.SoLuongNhap;
                }
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
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
