using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FwB.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Tên Món Quá Dài")]
        [Required(ErrorMessage = "{0} Không Được Để Trống")]
        [Display(Name = "Tên Món")]
        public string? ItemName { get; set; }
        [Display(Name = "Mô Tả")]
        public string? Description { get; set; }
        [Display(Name = "Giá Tiền")]
        [Required(ErrorMessage = "{0} Không Được Để Trống")]
        public double? Price { get; set; }

        [Display(Name = "Khuyến Mãi")]
        [Required(ErrorMessage = "{0} Không Được Để Trống")]
        public double? DiscountPrice { get; set; }


        public Menu? Menus { get; set; }

        public int? Status { get; set; }
        [Display(Name = "Is Food")]
        public int? isFood { get; set; }

        // [Display(Name = "Khuyến Mãi")]
        // public List<Discount>? Discount { get; set; }
    }
}