﻿using BookManagementWeb.Data;
using Microsoft.AspNetCore.Mvc;
using BookManagementWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Diagnostics.Eventing.Reader;

namespace BookManagementWeb.Controllers
{
    public class SachController : Controller
    {
        private readonly BookstoreDbContext _context;
        private readonly IWebHostEnvironment _webHost;

        public SachController(BookstoreDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        public IActionResult Index(string SearchString)
        {
            List<Sach> sachList = new List<Sach>();
            sachList = _context.SACH.ToList();
            if (!string.IsNullOrEmpty(SearchString))
            {
                sachList = sachList.Where(x => x.TenSach.Contains(SearchString)).ToList();
            }
            return View(sachList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Sach sach = new Sach();
            return View(sach);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Sach sach)
        {
            try
            {
                string uniqueFileName = GetProfilePhotoFileName(sach);
                sach.PhotoUrl = uniqueFileName;
               
                _context.SACH.Add(sach);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                return View();
            }
        }
        [HttpGet]
        public IActionResult GetGenres()
        {
            var genres = _context.SACH.Select(s => s.TheLoai).Distinct().ToList();
            return View("_GenresPartial", genres);
        }

        [HttpGet]
        public IActionResult GetBooksByGenre(string genre)
        {
            var booksByGenre = new List<Sach>();

            if (!string.IsNullOrEmpty(genre))
            {
                // Lọc sách theo thể loại
                booksByGenre = _context.SACH.Where(x => x.TheLoai == genre).ToList();
            }
            else
            {
                // Nếu thể loại không được chọn, hiển thị toàn bộ sách
                booksByGenre = _context.SACH.ToList();
            }

            return PartialView("_BooksPartial", booksByGenre);
        }
        [HttpGet]
        public IActionResult Edit(int maSach)
        {
            Sach sach = _context.SACH.FirstOrDefault(t => t.MaSach == maSach);
            return View(sach);


        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Edit(Sach sach)
        {
            if (sach.ProfilePhoto != null)
            {
                string uniqueFileName = GetProfilePhotoFileName(sach);
                sach.PhotoUrl = uniqueFileName;
            }
            _context.Attach(sach);
            _context.Entry(sach).State = EntityState.Modified;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Delete(int maSach)
        {
            var sach = _context.SACH.FirstOrDefault(t => t.MaSach == maSach);
            return View(sach);
        }


        [HttpPost]
        public IActionResult Delete(Sach sach)
        {
            _context.SACH.Remove(sach);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }

        private string GetProfilePhotoFileName(Sach sach)
        {
            string uniqueFileName = null;

            if (sach.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + sach.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    sach.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }



    }
}
