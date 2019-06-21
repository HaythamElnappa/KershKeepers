using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KershKeepers.Models.EFModels
{
    public class Order
    {
        [Key]
        [StringLength(50)]
        public string OrderId { get; set; }
        [Required]
        [StringLength(50)]
        public string UserId { get; set; }
        [Required]
        [ForeignKey("Provider")]
        [StringLength(50)]
        public string ProviderId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        [Column(TypeName = "Money")]
        public decimal TotalPrice { get; set; }
        public string Feedback { get; set; }
        [Required]
        public string Type { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual Provider Provider { get; set; }


    }
}