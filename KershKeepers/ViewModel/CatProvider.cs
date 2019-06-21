using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KershKeepers.Models.EFModels;

namespace KershKeepers.ViewModel
{
    public class CatProvider
    {
        public  List<Category> Categories { get; set; }
        public Provider Provider { get; set; }

    }
}