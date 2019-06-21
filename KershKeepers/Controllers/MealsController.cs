using KershKeepers.Models;
using KershKeepers.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KershKeepers.Controllers
{
    public class MealsController : Controller
    {
        ApplicationDbContext context;
        public MealsController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Meals
        public ActionResult Index()
        {
            //var query = context.OrdersDetails.SqlQuery("select top 2 MealId , sum(Quantity) as Quantity from OrderDetails group by MealId ORDER BY sum(Quantity) DESC").ToList<OrderDetails>();
     
            return View();
           
        }
        public ActionResult getMostSales()
        {
            var query = from o in context.OrdersDetails.AsEnumerable()

                        group o by o.MealId into e

                        select new OrderDetails
                        {
                            Quantity = e.Sum(x => x.Quantity),
                            MealId = e.First().MealId,
                            OrderId = e.First().OrderId,
                            Meal = e.First().Meal,
                            Order = e.First().Order
                           
                            
                        };




            var mostSales = query.OrderByDescending(ww => ww.Quantity).Take(25).ToList() ;
            
          
            return PartialView(mostSales);
        }
    }

}