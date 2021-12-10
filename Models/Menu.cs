using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FwB.Models
{
    public class Menu
    {
        [Key]
        [Display(Name = "ID")]
        public int MenuId { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Tên Menu Quá Dài")]
        [Required(ErrorMessage = "{0} Không Được Để Trống")]
        [Display(Name = "Tên Menu")]
        public string? MenuName { get; set; }


    }
}