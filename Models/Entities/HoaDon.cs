using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementWeb.Models.Entities
{
    public class HoaDon
    {
        [Required(ErrorMessage = "Vui lòng nhập mã hoá đơn")]
        [Key]
        public int MaHoaDon { get; set; }

        [Required(ErrorMessage = "Vui lòng ngày lập hoá đơn")]
        [Column(TypeName = "date")]
        public DateTime NgayLapHoaDon { get; set; }

        [ForeignKey("KhachHang")]
        public int MaKhachHang { get; set; }
        public virtual KhachHang KhachHang { get; set; }
    }
}
