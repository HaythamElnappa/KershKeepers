using KershKeepers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KershKeepers.Controllers
{
    public class ProviderController : Controller
    {
        ApplicationDbContext context;
        public ProviderController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Provider
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult getById(string id)
        {
            var provider = context.Providers.FirstOrDefault(ww=>ww.ProviderId == id.ToString());
            return PartialView(provider);
        }

        [HttpGet]
        public ActionResult AllProvider()
        {
            return View();
        }

        public ActionResult AllRestaurant()
        {
            var allrest = context.Providers.Where(n => n.Type == "resturant").ToList();
            ViewBag.tit = "Our Resaurant";
            return PartialView(allrest);
        }

        public ActionResult AllHomeProvider()
        {
            var allHomeProviders = context.Providers.Where(n => n.Type == "home food").ToList();
            ViewBag.tit = "Our Professional Chefs";
            return PartialView("AllRestaurant", allHomeProviders);
        }

    }
}