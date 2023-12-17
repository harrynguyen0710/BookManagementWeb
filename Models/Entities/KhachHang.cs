using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementWeb.Models.Entities
{
    public class KhachHang
    {
        [Required(ErrorMessage = "Vui lòng nhập mã khách hàng")]
        [Key]
        public int MaKhachHang { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ và tên khách hàng")]
        [Column(TypeName = "nvarchar(100)")]
        public string HoVaTen { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        public bool GioiTinh { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public string SDT { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string DiaChi { get; set; }
        public decimal SoTienNo { get; set; }

        public virtual ICollection<HoaDon> HoaDonCollectionKH {  get; set; }

    }
}
