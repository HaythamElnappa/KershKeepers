using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KershKeepers.Models.EFModels
{
    public class Category
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(300)]
        public string Image { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<Meal> Meals { get; set; }

        public Category()
        {
            Meals = new HashSet<Meal>();
        }

    }
}