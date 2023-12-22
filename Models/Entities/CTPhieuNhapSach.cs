/* using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementWeb.Models.Entities
{
    public class CTPhieuNhapSach
    {
        [Key]
        [ForeignKey("PhieuNhapSach")]
        public int MaPhieuNhapSach { get; set; }

        [Key]
        [ForeignKey("Sach")]
        public int MaSach { get; set; }

        [Required(ErrorMessage = "Vui lòng điền số lượng sách nhập")]
        public int SoLuongNhap { get; set; }

        [Required(ErrorMessage = "Vui lòng điền đơn giá nhập")]
        public decimal DonGiaNhap { get; set; }

        public virtual Sach Sach { get; set; }
        public virtual PhieuNhapSach PhieuNhapSach { get; set; }
    }
}
*/