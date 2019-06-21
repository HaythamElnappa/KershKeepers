using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KershKeepers.Models.EFModels
{
    public class Provider
    {
        [Key]
        [StringLength(50)]
        public string ProviderId { get; set; }
        [Required]
        [StringLength(300)]
        public string Name { get; set; }
        [Required]
        [StringLength(300)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(300)]
        public string Password { get; set; }
        [Required]
        //[RegularExpression("(010|011|012|015)//d{8}",ErrorMessage = "Enter A valid Phone Number")]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [StringLength(300)]
        public string Image { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [Required]
        //[Column(TypeName = "Time")]
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime WorkStartTime { get; set; }
        [Required]
        //[Column(TypeName = "Time")]
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime WorkEndTime { get; set; }
        [Column(TypeName = "Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime RegisterDate { get; set; }

        public bool IsActivated { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("Area")]
        public int? AreaId { get; set; }
        public Area Area { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

        public Provider()
        {
            Meals = new HashSet<Meal>();
            Orders = new HashSet<Order>();
            Messages = new HashSet<Message>();
        }

    }
}