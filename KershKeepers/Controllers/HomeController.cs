using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KershKeepers.Models;

namespace KershKeepers.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context;
        public HomeController()
        {
            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var cities = context.Cities.ToList();
            var areas = context.Areas.Where(n => n.CityId == 1);
            ViewBag.cityList = new SelectList(cities, "CityId", "Name", 1);
            ViewBag.areaList = new SelectList(areas, "AreaId", "Name", new { n = "select" });
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //////////////////////////////////////////////////
        
        public JsonResult AreasByCityId(int id)
        {
            var Areas = context.Areas.Where(n => n.CityId == id).ToList();
            return Json(Areas, JsonRequestBehavior.AllowGet);

        }

        //public ActionResult HomeProviderByArea()
        //{
        //    int id = 1;
        //    //int id =int.Parse(Request.Form["AreaId"].ToString());
            

        //    var providerList = context.Providers.Where(n => n.AreaId == id && n.Type == "home food");
        //    return View(providerList);
        //}

        public ActionResult RestaurantByArea()
        {
            int id = 1;
            //int id =int.Parse(Request.Form["AreaId"].ToString());


            var providerList = context.Providers.Where(n => n.AreaId == id && n.Type == "restaurant");
            return View(providerList);
        }

        public ActionResult Search()
        {
            string providerType = Request.Form["exampleRadios"].ToString();
            int areaId =int.Parse(Request.Form["AreaId"].ToString());
            var providerList = context.Providers.Where(n => n.AreaId == areaId && n.Type == providerType);

            return View(providerList);
        }


    }
}