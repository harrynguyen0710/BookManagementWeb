using BookManagementWeb.Data;
using Microsoft.AspNetCore.Mvc;

public class BaoCaoCongNoController : Controller
{
    private readonly BookstoreDbContext _context;

    public BaoCaoCongNoController(BookstoreDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int? selectedMonth, int? selectedYear)
    {
        var baoCaoData = GetBaoCaoCongNoData(selectedMonth, selectedYear);
        return View(baoCaoData);
    }

    private IQueryable<(int MaKhachHang, string TenKhachHang, decimal? NoDauThang, decimal? NoCuoiThang)> GetBaoCaoCongNoData(int? selectedMonth, int? selectedYear)
    {
        // Add logic to filter data based on selectedMonth and selectedYear
        var baoCaoData = _context.KHACHHANG
            .Select(khachHang => new
            {
                MaKhachHang = khachHang.MaKhachHang,
                TenKhachHang = khachHang.HoVaTen,
                NoDauThang = TinhNoDauThang(khachHang.MaKhachHang, selectedMonth, selectedYear, _context),
                NoCuoiThang = TinhNoCuoiThang(khachHang.MaKhachHang, selectedMonth, selectedYear, _context)
            });

        return baoCaoData.Select(item => new ValueTuple<int, string, decimal?, decimal?>(
            item.MaKhachHang,
            item.TenKhachHang,
            item.NoDauThang,
            item.NoCuoiThang
        ));
    }

    private static decimal? TinhNoDauThang(int maKhachHang, int? selectedMonth, int? selectedYear, BookstoreDbContext context)
    {
        // Add logic to filter data based on selectedMonth and selectedYear
        var noCuoiThangThangTruoc = context.PHIEUTHUTIEN
            .Where(pt => pt.HoaDon.MaKhachHang == maKhachHang && pt.NgayLapPhieuThuTien.Month == selectedMonth-1 && pt.NgayLapPhieuThuTien.Year == selectedYear)
            .OrderByDescending(pt => pt.NgayLapPhieuThuTien)
            .Select(pt => pt.SoTienKhachNo)
            .FirstOrDefault();

        return noCuoiThangThangTruoc;
    }

    private static decimal? TinhNoCuoiThang(int maKhachHang, int? selectedMonth, int? selectedYear, BookstoreDbContext context)
    {
        // Add logic to filter data based on selectedMonth and selectedYear
        var noCuoiThangHienTai = context.PHIEUTHUTIEN
            .Where(pt => pt.HoaDon.MaKhachHang == maKhachHang && pt.NgayLapPhieuThuTien.Month == selectedMonth && pt.NgayLapPhieuThuTien.Year == selectedYear)
            .OrderByDescending(pt => pt.NgayLapPhieuThuTien)
            .Select(pt => pt.SoTienKhachNo)
            .FirstOrDefault();

        // Kiểm tra xem có phiếu thu tiền trong tháng hiện tại không
        if (noCuoiThangHienTai == null)
        {
            // Lặp lại cho đến khi tìm thấy giá trị có sẵn
            while (selectedMonth > 0)
            {
                selectedMonth--;

                var noCuoiThangTruoc = context.PHIEUTHUTIEN
                    .Where(pt => pt.HoaDon.MaKhachHang == maKhachHang && pt.NgayLapPhieuThuTien.Month == selectedMonth && pt.NgayLapPhieuThuTien.Year == selectedYear)
                    .OrderByDescending(pt => pt.NgayLapPhieuThuTien)
                    .Select(pt => pt.SoTienKhachNo)
                    .FirstOrDefault();

                if (noCuoiThangTruoc != null)
                {
                    return noCuoiThangTruoc;
                }
            }

            // Nếu không có giá trị nào được tìm thấy, bạn có thể trả về null hoặc giá trị mặc định khác
            return null;
        }

        return noCuoiThangHienTai;
    }

}
