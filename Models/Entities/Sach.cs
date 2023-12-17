using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookManagementWeb.Models.Entities
{
    public class Sach
    {
        [Required(ErrorMessage = "Vui lòng nhập mã sách")]
        [Key]
        public int MaSach { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sách")]
        [Column(TypeName = "nvarchar(100)")]
        public string TenSach { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên tác giả")]
        [Column(TypeName = "nvarchar(100)")]
        public string TacGia { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập thể loại của sách")]
        [Column(TypeName = "nvarchar(100)")]
        public string TheLoai { get; set; }
        public int SoLuongSach { get; set; }

        [Required(ErrorMessage = "Vui lòng thêm ảnh cho sách")]
        public string PhotoUrl { get; set; }

        [NotMapped]
        public IFormFile ProfilePhoto { get; set; }

    }
}
