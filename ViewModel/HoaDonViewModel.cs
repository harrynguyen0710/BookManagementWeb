using BookManagementWeb.Models.Entities;

namespace BookManagementWeb.ViewModel
{
    public class HoaDonViewModel
    {
        public int MaHoaDon { get; set; }
        public DateTime NgayLapHoaDon { get; set; }

        public int MaKhachHang { get; set; }
        public int MaSach { get; set; }

       
        public int SoLuongBan { get; set; }

        public decimal DonGiaBan { get; set; }
        public List<HoaDonViewModel> HDSachList { get; set; }


        


    }
}
