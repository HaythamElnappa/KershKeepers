using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;
using KershKeepers.Models;
using KershKeepers.Models.EFModels;

namespace KershKeepers.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            if (Session["ProviderId"] != null)
            {
                ViewBag.providerId = Session["ProviderId"].ToString();
                return View();
            }
            else
            {
                return HttpNotFound();
            }
            
        }

        [HttpGet]
        public ActionResult GetAll(string id)
        {
            var orderlist = db.Orders.Where(ww=>ww.ProviderId==id);
            return PartialView(orderlist);
        }

        public ActionResult GetOrderDetails(string id)
        {
            var details = db.OrdersDetails.Where(ww => ww.Order.OrderId == id );  
            return PartialView(details);  
        }

        public ActionResult OrderRej(string id)
        {
            var dd = db.Orders.FirstOrDefault(ww => ww.OrderId == id);
            dd.Status = "Rejected";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderAccepted(string id)
        {
            var dd = db.Orders.FirstOrDefault(ww => ww.OrderId == id);
            dd.Status = "Accepted";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderDelevired(string id)
        {
            var dd = db.Orders.FirstOrDefault(ww => ww.OrderId == id);
            dd.Status = "Delevired";
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        ///////////////////////////////////////////////

        public ActionResult IndexUserOrder()
        {
            return View();
        }

        public ActionResult GetAllForUser(string id)
        {
            var order = db.Orders.Where(ww => ww.User.Id == id);

            return PartialView(order);
        }

        public ActionResult GetDetailsOrderForUser(string id)
        {
            var order = db.OrdersDetails.Where(ww => ww.Order.OrderId==id);
           // ViewBag.order = db.OrdersDetails.FirstOrDefault(ww=>ww.Order.OrderId==id).Order.Status;
            return PartialView(order);
        }

        public ActionResult FeedBack(string id,string feedback)
        {
            var order = db.Orders.Find(id);
            order.Feedback = feedback;
            db.Entry(order).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("IndexUserOrder");
        }
    }
}