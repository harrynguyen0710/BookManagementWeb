using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookManagementWeb.Models.Entities
{
    public class ThayDoiQuyDinh
    {
        [Key]
        public int LanThayDoi { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn tối thiểu trước khi nhập phải là số nguyên không âm.")]
        public int MinTonTruocNhap { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn tối thiểu sau khi bán phải là số nguyên không âm.")]
        public int MinTonSauBan { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Số nợ tối đa phải là số không âm.")]
        public decimal MaxNo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng nhập tối thiểu phải là số nguyên dương.")]
        public int MinNhap { get; set; }
        public bool ApDung { get; set; }
    }
}
