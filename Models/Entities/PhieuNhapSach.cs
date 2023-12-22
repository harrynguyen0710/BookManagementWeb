/* using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementWeb.Models.Entities
{
    public class PhieuNhapSach
    {
        [Required(ErrorMessage = "Vui lòng nhập phiếu nhập sách")]
        [Key]
        public int MaPhieuNhapSach { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thời gian lập phiếu")]
        [Column(TypeName = "date")]
        public DateTime NgayLapPhieuNhap { get; set; }

        public virtual ICollection<CTPhieuNhapSach> CTPhieuNhapSachPN { get; set; }

    }
}
*/