using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FwB.Models
{
    public class ItemByIdOrder
    {
        [Key]
        [Display(Name = "ID")]
        public int ItemByIdOrderID { get; set; }

        public Order? Orders { get; set; }
        public Item? Items { get; set; }

    }
}