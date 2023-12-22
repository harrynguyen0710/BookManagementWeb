using BookManagementWeb.Data;
using BookManagementWeb.Models.Entities;
using BookManagementWeb.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementWeb.Controllers
{
    public class CTPhieuNhapSachController : Controller
    {
        private readonly BookstoreDbContext _context;
        public CTPhieuNhapSachController(BookstoreDbContext context)
        {
            _context = context;
        }

        public IActionResult Details(int maPhieuNhapSach)
        {
            List<int> ttMaSach = new List<int>();
            List<Sach> sachList = new List<Sach>();
            List<CTPhieuNhapSachViewModel> ctPNList = new List<CTPhieuNhapSachViewModel>();
            var phieuNhap = _context.PHIEUNHAPSACH.Where(x => x.MaPhieuNhapSach == maPhieuNhapSach).FirstOrDefault();
            var CTPNList = _context.CTPHIEUNHAPSACH.Where(x => x.MaPhieuNhapSach == maPhieuNhapSach).ToList();


            foreach (var i in CTPNList)
            {
                ttMaSach.Add(i.MaSach);
            }

            int j = 0;
            foreach (var i in CTPNList)
            {
                CTPhieuNhapSachViewModel ctPN = new CTPhieuNhapSachViewModel();
                ctPN.MaPhieuNhapSach = phieuNhap.MaPhieuNhapSach;
                ctPN.NgayLapPhieuNhap = phieuNhap.NgayLapPhieuNhap;
                ctPN.SoLuongNhap = i.SoLuongNhap;
                ctPN.DonGiaNhap = i.DonGiaNhap;
                ctPN.MaSach = ttMaSach[j];
                ctPN.TenSach = _context.SACH.Where(x => x.MaSach == ctPN.MaSach).Select(x => x.TenSach).FirstOrDefault();
                ctPN.TacGia = _context.SACH.Where(x => x.MaSach == ctPN.MaSach).Select(x => x.TacGia).FirstOrDefault();
                ctPN.TheLoai = _context.SACH.Where(x => x.MaSach == ctPN.MaSach).Select(x => x.TheLoai).FirstOrDefault();

                ctPNList.Add(ctPN);
                j += 1;
            }


            return View(ctPNList);
        }
    }
}
