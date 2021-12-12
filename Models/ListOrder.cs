using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FwB.Models
{
    public class ListOrder
    {
        [Key]
        [Display(Name = "ID")]
        
        public int ListOrderId { get; set; }

        public Order? Orders { get; set; }

    }
}