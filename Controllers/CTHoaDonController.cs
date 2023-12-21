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
          /*  List<>*/
/*            CTHoaDonViewModel ctHD = new CTHoaDonViewModel();
*/            var hoaDon = _context.HOADONBANSACH.Where(x => x.MaHoaDon == maHoaDon).FirstOrDefault();
            var phieuThuTien = _context.PHIEUTHUTIEN.Where(x => x.MaHoaDon == maHoaDon).FirstOrDefault();
            var CTHDList = _context.CTHOADONBANSACH.Where(x => x.MaHoaDon == maHoaDon).ToList();
            foreach(var i in CTHDList)
            {
                ttMaSach.Add(i.MaSach);
            }

            for(int i = 0; i < ttMaSach.Count; i++)
            {
                var sach = _context.SACH.Where(x => x.MaSach == ttMaSach[i]).FirstOrDefault();
                sachList.Add(sach);
            }

            foreach(var i in sachList)
            {


            }


            return View();
        }
    }
}
