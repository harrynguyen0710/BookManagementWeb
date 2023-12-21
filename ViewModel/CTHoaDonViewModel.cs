using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementWeb.ViewModel
{
    public class CTHoaDonViewModel
    {
        public int MaHoaDon { get; set; }
        public DateTime NgayLapHoaDon { get; set; }
        public int MaKhachHang { get; set; }

        // CT HD + (Ma hoa don)
        public int SoLuongBan { get; set; }
        public decimal DonGiaBan { get; set; }

        public decimal SoTienKhachNo { get; set; }
        public decimal TongSoTienThu { get; set; }

        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string TacGia { get; set; }
        public string TheLoai { get; set; }
        public int SoLuongSach { get; set; }
        public string PhotoUrl { get; set; }
        public IFormFile ProfilePhoto { get; set; }

        List<CTHoaDonViewModel> cthdVMList { get; set; }



    }
}
