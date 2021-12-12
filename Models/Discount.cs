using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FwB.Models
{
    public class Discount
    {
        [Key]
        [Display(Name = "ID")]
        public int DiscountId { get; set; }
        // [ForeignKey("FK_Menu")]
        // public int? MenuId { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Tên Khuyến Mãi Quá Dài")]
        [Required(ErrorMessage = "{0} Không Được Để Trống")]
        [Display(Name = "Tên Khuyến Mãi")]
        public string? DiscountName { get; set; }

        [Display(Name = "Nội Dung Khuyến Mãi")]
        [Required(ErrorMessage = "{0} Không Được Để Trống")]
        public string? Content { get; set; }

        public int? Status { get; set; }
        public Item? Items { get; set; }
        public int? Value { get; set; }


    }
}