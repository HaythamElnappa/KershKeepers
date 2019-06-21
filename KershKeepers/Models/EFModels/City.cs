using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KershKeepers.Models.EFModels
{
    public class City
    {
        public int CityId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        public ICollection<Area> Areas { get; set; }

        public City()
        {
            Areas = new HashSet<Area>();
        }
    }
}