using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KershKeepers.Models.EFModels
{
    public class Cart
    {
        [Key,Column(Order = 0)]
        [StringLength(50)]
        public string UserId { get; set; }
        [Key,Column(Order = 1)]
        [ForeignKey("Meal")]
        public int MealId { get; set; }
        [ForeignKey("Provider")]
        [StringLength(50)]
        public string ProviderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual Meal Meal { get; set; }
        public virtual Provider Provider { get; set; }

    }
}