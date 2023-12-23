using BookManagementWeb.Data;
using BookManagementWeb.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class ThayDoiQuyDinhController : Controller
{
    private readonly BookstoreDbContext _context;

    public ThayDoiQuyDinhController(BookstoreDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var thayDoiQuyDinh = await _context.THAYDOIQUYDINH.ToListAsync();
        return View(thayDoiQuyDinh);
    }
/*
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("LanThayDoi,MinNhap,MinTonTruocNhap,MaxNo,MinTonSauBan,TinhTrang")] ThayDoiQuyDinh thayDoiQuyDinh)
    {
        if (ModelState.IsValid)
        {
            _context.Add(thayDoiQuyDinh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(thayDoiQuyDinh);
    }*/

    public IActionResult Edit()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ThayDoiQuyDinh thayDoiQuyDinh)
    {
        thayDoiQuyDinh.LanThayDoi = 1;
        _context.Attach(thayDoiQuyDinh);
        _context.Entry(thayDoiQuyDinh).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
        
    }

    private bool ThayDoiQuyDinhExists(int id)
    {
        return _context.THAYDOIQUYDINH.Any(e => e.LanThayDoi == id);
    }
}
