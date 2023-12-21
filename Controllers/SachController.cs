using BookManagementWeb.Data;
using Microsoft.AspNetCore.Mvc;
using BookManagementWeb.Models.Entities;
using Microsoft.EntityFrameworkCore;

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

        public IActionResult Index()
        {
            List<Sach> sachList = new List<Sach>();
            sachList = _context.SACH.ToList();
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
                // Log the exception for debugging purposes.
                Console.WriteLine(ex.Message);
                return View();
            }
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
