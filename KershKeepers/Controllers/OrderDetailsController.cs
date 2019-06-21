using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KershKeepers.Models;
using KershKeepers.Models.EFModels;

namespace KershKeepers.Controllers
{
    public class OrderDetailsController : Controller
    {
        // GET: OrderDetails
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetOrderByUserId(string id)
        {
            var orderDetails = db.OrdersDetails.Where(ww => ww.Order.UserId == id).ToList();
            return PartialView( orderDetails);
        }

        public ActionResult IndexProvider()
        {
            return View();
        }

        public ActionResult GetOrderByProviderId(string id)
        {
            var orderDetails = db.OrdersDetails.Where(ww => ww.Order.ProviderId == id).ToList();
            return PartialView(orderDetails);
        }
    }
}