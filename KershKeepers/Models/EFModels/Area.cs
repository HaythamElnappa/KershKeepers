using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace KershKeepers.Models.EFModels
{
    public class Area
    {
        public int AreaId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; }

        public virtual ICollection<Provider> Providers { get; set; }

        public Area()
        {
            Providers = new HashSet<Provider>();
        }
    }
}