using BookManagementWeb.Models.Entities;

namespace BookManagementWeb.ViewModel
{
    public class PhieuNhapViewModel
    {
        public List<int> maPhieuNhapSach {  get; set; }
        public List<int> maSach {  get; set; }
        public List<string> tenSach { get; set; }
        public List<int> soLuongNhap { get; set; }
        public List<decimal> donGiaNhap { get; set; }
        public  List<DateTime> ngayLapPhieuNhap { get; set; }

        public List<PhieuNhapViewModel> phieuNhapViewModels { get; set; } = new List<PhieuNhapViewModel>();
    }
}
