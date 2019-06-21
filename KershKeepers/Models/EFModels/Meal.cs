using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KershKeepers.Models.EFModels
{
    public class Meal
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MealId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        [StringLength(300)]
        public string Image { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal")]
        public decimal? ExecutionTime { get; set; }
        public bool Available { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [ForeignKey("Provider")]
        public string ProviderId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Provider Provider { get; set; }


    }
}