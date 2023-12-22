using BookManagementWeb.Data;
using Microsoft.AspNetCore.Mvc;

public class BaoCaoTonKhoController : Controller
{
    private readonly BookstoreDbContext _context;

    public BaoCaoTonKhoController(BookstoreDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int? selectedMonth, int? selectedYear)
    {
        var baoCaoData = GetBaoCaoTonKhoData(selectedMonth, selectedYear);
        return View(baoCaoData);
    }

    private IQueryable<(int MaSach, string TenSach, int SoLuongDauThang, int SoLuongCuoiThang)> GetBaoCaoTonKhoData(int? selectedMonth, int? selectedYear)
    {
        var baoCaoData = _context.SACH
            .Select(sach => new
            {
                MaSach = sach.MaSach,
                TenSach = sach.TenSach,
                SoLuongDauThang = TinhSoLuongDauThang(sach.MaSach, selectedMonth, selectedYear, _context),
                SoLuongCuoiThang = TinhSoLuongCuoiThang(sach.MaSach, selectedMonth, selectedYear, _context)
            });

        return baoCaoData.Select(item => new ValueTuple<int, string, int, int>(
            item.MaSach,
            item.TenSach,
            item.SoLuongDauThang,
            item.SoLuongCuoiThang
        ));
    }

    private static int TinhSoLuongDauThang(int maSach, int? selectedMonth, int? selectedYear, BookstoreDbContext context)
    {
        // Add logic to filter data based on selectedMonth and selectedYear
        var soLuongNhap = context.CTPHIEUNHAPSACH
            .Where(ctpn => ctpn.MaSach == maSach && ctpn.PhieuNhapSach.NgayLapPhieuNhap.Month == selectedMonth - 1 && ctpn.PhieuNhapSach.NgayLapPhieuNhap.Year == selectedYear)
            .Select(ctpn => ctpn.SoLuongNhap)
            .Sum();

        var soLuongBan = context.CTHOADONBANSACH
            .Where(cthd => cthd.MaSach == maSach && cthd.HoaDon.NgayLapHoaDon.Month == selectedMonth - 1 && cthd.HoaDon.NgayLapHoaDon.Year == selectedYear)
            .Select(cthd => cthd.SoLuongBan)
            .Sum();

        var soLuongDauThang = soLuongNhap - soLuongBan;

        return soLuongDauThang < 0 ? 0 : soLuongDauThang; // Đảm bảo số lượng không âm
    }

    private static int TinhSoLuongCuoiThang(int maSach, int? selectedMonth, int? selectedYear, BookstoreDbContext context)
    {
        // Add logic to filter data based on selectedMonth and selectedYear
        var soLuongNhap = context.CTPHIEUNHAPSACH
            .Where(ctpn => ctpn.MaSach == maSach && ctpn.PhieuNhapSach.NgayLapPhieuNhap.Month == selectedMonth && ctpn.PhieuNhapSach.NgayLapPhieuNhap.Year == selectedYear)
            .Select(ctpn => ctpn.SoLuongNhap)
            .Sum();

        var soLuongBan = context.CTHOADONBANSACH
            .Where(cthd => cthd.MaSach == maSach && cthd.HoaDon.NgayLapHoaDon.Month == selectedMonth && cthd.HoaDon.NgayLapHoaDon.Year == selectedYear)
            .Select(cthd => cthd.SoLuongBan)
            .Sum();

        var soLuongCuoiThang = soLuongNhap - soLuongBan;

        // Kiểm tra xem có phiếu nhập sách trong tháng hiện tại không
        if (soLuongCuoiThang < 0)
        {
            // Lặp lại cho đến khi tìm thấy giá trị có sẵn
            while (selectedMonth > 0)
            {
                selectedMonth--;

                var soLuongCuoiThangTruoc = context.CTPHIEUNHAPSACH
                    .Where(ctpn => ctpn.MaSach == maSach && ctpn.PhieuNhapSach.NgayLapPhieuNhap.Month == selectedMonth && ctpn.PhieuNhapSach.NgayLapPhieuNhap.Year == selectedYear)
                    .Select(ctpn => ctpn.SoLuongNhap)
                    .Sum();

                var soLuongBanThangTruoc = context.CTHOADONBANSACH
                    .Where(cthd => cthd.MaSach == maSach && cthd.HoaDon.NgayLapHoaDon.Month == selectedMonth && cthd.HoaDon.NgayLapHoaDon.Year == selectedYear)
                    .Select(cthd => cthd.SoLuongBan)
                    .Sum();

                soLuongCuoiThang = soLuongCuoiThangTruoc - soLuongBanThangTruoc;

                if (soLuongCuoiThang >= 0)
                {
                    return soLuongCuoiThang;
                }
            }

            // Nếu không có giá trị nào được tìm thấy, bạn có thể trả về 0 hoặc giá trị mặc định khác
            return 0;
        }

        return soLuongCuoiThang;
    }


}
