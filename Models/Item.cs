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
        [Display(Name = "Menu")]
        public int? MenuId { get; set; }
        [ForeignKey("FK_MENUID")]
        public List<Menu>? Menus { get; set; }


        // [Display(Name = "Khuyến Mãi")]
        // public List<Discount>? Discount { get; set; }
    }
}