using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KershKeepers.Models.EFModels
{
    public class OrderDetails
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Order")]
        [StringLength(50)]
        public string OrderId { get; set; }
        [Key, Column(Order = 1)]
        [ForeignKey("Meal")]
        public int MealId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public int? Rate { get; set; }

        public virtual Order Order { get; set; }
        public virtual Meal Meal { get; set; }

    }
}