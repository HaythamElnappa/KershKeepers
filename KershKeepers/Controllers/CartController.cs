using KershKeepers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KershKeepers.Models.EFModels;
using Microsoft.AspNet.Identity;

namespace KershKeepers.Controllers
{

    public class CartController : Controller
    {
        ApplicationDbContext context;
        public CartController()
        {
            context = new ApplicationDbContext();
        }
        //[Authorize(Roles = "User")]
        //[Authorize]
        public ActionResult AddToCart(int id)
        {
            var meal = context.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (User.Identity.GetUserId() == null)
                {
                    return RedirectToAction("Login", "Account",routeValues: new {returnUrl=$"/Meal/Index/{meal.ProviderId}"});
                }

                string userId = User.Identity.GetUserId();
                var cart = context.Carts.FirstOrDefault(ww => ww.UserId == userId);
                if (cart == null)
                {
                    Cart userCart = new Cart()
                    {
                        MealId = meal.MealId,
                        ProviderId = meal.ProviderId,
                        UserId = userId,
                        Quantity = 1
                    };
                    context.Carts.Add(userCart);
                    context.SaveChanges();

                }
                else
                {
                    var cartItem = context.Carts.Find(userId, id);
                    if (cartItem == null)
                    {
                        cartItem = new Cart();
                        cartItem.MealId = id;
                        cartItem.ProviderId = meal.ProviderId;
                        cartItem.Quantity = 1;
                        cartItem.UserId = userId;
                        context.Entry(cartItem).State = System.Data.Entity.EntityState.Added;
                        context.SaveChanges();
                    }
                    else if(cartItem!=null)
                    {

                        cartItem.Quantity+=1;
                        context.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
                        context.SaveChanges();
                    }
                }
              
            
            }
            


            return RedirectToAction("Index","Meal",routeValues: new {id=meal.ProviderId});

           
        }

        //---------------Haytham-------------------------

        public ActionResult Home(string id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult CartDetails(string id)
        {
            var carts = context.Carts.Where(nn => nn.UserId == id).ToList();
            if (carts.Count == 0)
            {
                return PartialView("EmptyCart");
            }
            return PartialView(carts);
        }

        public ActionResult increaseQuantity(string userId, string mealId)
        {
            //int m = int.Parse(mealId);
            var cart = context.Carts.FirstOrDefault(n => n.UserId == userId && n.MealId.ToString() == mealId);
            cart.Quantity++;
            context.SaveChanges();
            var carts = context.Carts.Where(nn => nn.UserId == userId).ToList();

            return PartialView("CartDetails", carts);
        }

        public ActionResult decreaseQuantity(string userId, string mealId)
        {
            var cart = context.Carts.FirstOrDefault(n => n.UserId == userId && n.MealId.ToString() == mealId);
            cart.Quantity--;
            context.SaveChanges();
            var carts = context.Carts.Where(nn => nn.UserId == userId).ToList();
            return PartialView("CartDetails", carts);
        }

        [HttpDelete]
        public ActionResult delete(string userId, string mealId)
        {
            context.Carts.Remove(context.Carts.FirstOrDefault(n => n.UserId == userId && n.MealId.ToString() == mealId));
            context.SaveChanges();
            var carts = context.Carts.Where(nn => nn.UserId == userId).ToList();
            return PartialView("CartDetails", carts);
        }

        public void Checkout(string userId , string providerId , string totalPrice)
        {
            var totalpric = decimal.Parse(totalPrice);
            var order = new Order { OrderId = "2", UserId = userId , IsDeleted= false , ProviderId = providerId , Date = DateTime.Now , Type = "order" , TotalPrice = totalpric, Status = "Pending" };
            context.Orders.Add(order);
            context.SaveChanges();
            var cartitems = context.Carts.Where(n => n.UserId == userId).ToList();
            for (int i = 0; i < cartitems.Count; i++)
            {
                var oId = context.Orders.ToList().Last().OrderId;
                context.OrdersDetails.Add(new OrderDetails { MealId = cartitems[i].MealId, Quantity = cartitems[i].Quantity, OrderId = oId });
                context.SaveChanges();
            }
            context.Carts.RemoveRange(context.Carts.Where(n => n.UserId == userId));
            context.SaveChanges();

        }

    }
}