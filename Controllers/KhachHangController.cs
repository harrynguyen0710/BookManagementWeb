using BookManagementWeb.Data;
using Microsoft.AspNetCore.Mvc;
using BookManagementWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using X.PagedList.Mvc.Core;
using X.PagedList.Mvc;
using X.PagedList;

namespace BookManagementWeb.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly BookstoreDbContext _context;

        private readonly IWebHostEnvironment _webHost;

        public KhachHangController(BookstoreDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Index(int? page)
        {
            var pageNumber = page ?? 1; 
            var pageSize = 5; 

            var khList = _context.KHACHHANG.ToPagedList(pageNumber, pageSize);

            return View(khList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            KhachHang khachHang = new KhachHang();
            return View(khachHang);
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(KhachHang khachHang)
        {
            //try
            //{
                //if (ModelState.IsValid)
                //{
                    _context.KHACHHANG.Add(khachHang);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                //}
                // If ModelState is not valid, return to the view with validation errors.
                //return View(khachHang);
            //}
            /*catch (Exception ex)
            {
                // Log the exception for debugging purposes.
                Console.WriteLine(ex.Message);
                return View();
            }*/
        }

        [HttpGet]
        public IActionResult Edit(int maKhachHang)
        {
            KhachHang khachHang = _context.KHACHHANG.FirstOrDefault(t => t.MaKhachHang == maKhachHang);
            return View(khachHang);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(KhachHang khachHang)
        {
            _context.Attach(khachHang);
            _context.Entry(khachHang).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        /*
        [HttpGet]
        public IActionResult Delete(int maKhachHang)
        {
            KhachHang khachHang = _context.KHACHHANG.FirstOrDefault(t => t.MaKhachHang == maKhachHang);
            return View(khachHang);
        }

        [HttpPost]
        public IActionResult Delete(KhachHang khachHang)
        {
            _context.KHACHHANG.Remove(khachHang);
            _context.SaveChanges();
            return RedirectToAction("Index");
        } */
    }
}
