namespace BookManagementWeb.ViewModel
{
    public class CTPhieuNhapSachViewModel
    {
        public int MaPhieuNhapSach { get; set; }
        public DateTime NgayLapPhieuNhap { get; set; }
        public int MaSach { get; set; }
        public int SoLuongNhap {  get; set; }
        public string TenSach { get; set; }
        public string TacGia {  get; set; }
        public string TheLoai {  get; set; }
        public decimal DonGiaNhap { get; set; }

        public List<CTPhieuNhapSachViewModel> ctPhieuNhapViewModel {  get; set; }
    }
}
