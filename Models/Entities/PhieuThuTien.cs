using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementWeb.Models.Entities
{
    public class PhieuThuTien
    {
        [Required(ErrorMessage = "Vui lòng nhập mã phiếu thu tiền")]
        [Key]
        public int MaPhieuThuTien { get; set; }
        [ForeignKey("HoaDon")]
        public int MaHoaDon { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tổng số tiền thu")]
        public decimal TongSoTienThu { get; set; }
        [Column(TypeName = "date")]

        [Required(ErrorMessage = "Vui lòng nhập thời gian thu tiền")]
        public DateTime NgayLapPhieuThuTien { get; set; }
        /// Số tiền khách nợ tại thời điểm mua hàng
        public decimal SoTienKhachNo { get; set; }

        public virtual HoaDon HoaDon { get; set; }

    }
}
