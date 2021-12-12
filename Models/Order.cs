using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FwB.Models
{
    public class Order
    {
        [Key]
        [Display(Name = "ID")]
        public int OrderId { get; set; }


        [ForeignKey("FK_MENUID")]
        public List<Menu>? Menus { get; set; }

        public int? OrderItemId { get; set; }
        [ForeignKey("FK_ITEMID")]
        public Item? Items { get; set; }

        [ForeignKey("FK_DISCOUNTID")]
        public Discount? Discounts { get; set; }
        [Display(Name = "Số Lượng")]
        public int Quantity { get; set; }

        public int? IdToGetListOrder { get; set; }

        public DateTime CreateDate { get; set; }
    }
}