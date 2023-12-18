using BookManagementWeb.Data;
using BookManagementWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementWeb.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly BookstoreDbContext _Context;

        public KhachHangController(BookstoreDbContext context)
        {
            _Context = context;
        }
        public IActionResult Index()
        {
            List<KhachHang> khachHang;
            khachHang = _Context.KHACHHANG.ToList();
            return View(khachHang);
        }

        [HttpPost]
        public IActionResult Create(KhachHang khachHang)
        {
            _Context.Add(khachHang);
            _Context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
