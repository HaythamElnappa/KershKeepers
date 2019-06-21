using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KershKeepers.Models.EFModels
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MessageId { get; set; }
        [StringLength(50)]
        public string UserId { get; set; }
        [StringLength(50)]
        [ForeignKey("Provider")]
        public string ProviderId { get; set; }
        [Required]
        public string MessageBody { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsOpened { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public virtual Provider Provider { get; set; }
    }
}