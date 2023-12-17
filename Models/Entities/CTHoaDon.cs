using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementWeb.Models.Entities
{
    public class CTHoaDon
    {

        [ForeignKey("HoaDon")]
        public int MaHoaDon { get; set; }

        [ForeignKey("Sach")]
        public int MaSach { get; set; }

        [Required(ErrorMessage = "Vui lòng điền số lượng bán")]
        public int SoLuongBan { get; set; }

        [Required(ErrorMessage = "Vui lòng điền đơn giá bán")]
        public decimal DonGiaBan { get; set; }

        public virtual HoaDon HoaDon { get; set; }
        public virtual Sach Sach { get; set; }

       

    }
}
