using KershKeepers.Models;
using KershKeepers.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KershKeepers.ViewModel;

namespace KershKeepers.Controllers
{
    public class MealController : Controller
    {
        ApplicationDbContext context;
        public MealController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Meal
        public ActionResult Index(string id)
        {
          
            CatProvider catProvider = new CatProvider()
            {
                Categories = context.Categories.ToList(),
                Provider = context.Providers.Find(id)
                  
        };

           return View(catProvider);
            
      
         
        }
        [HttpGet]
        public ActionResult GetAll(string id ="1")
        {
            var meal = context.Meals.Where(ww=>ww.ProviderId ==id).ToList();
            return PartialView(meal);
        }
    
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var meal = context.Meals.FirstOrDefault(ww => ww.MealId == id);
            context.Meals.Remove(meal);
            context.SaveChanges();

            return PartialView("GetAll", context.Meals.ToList());


        }
      
        public ActionResult getByCategories(int id =3,string providerId = "1")
        {
            var meal = context.Meals.Where(ww=>ww.CategoryId==id).Where(ww=>ww.ProviderId==providerId).ToList();

            return PartialView(meal);
        }









        public ActionResult ProviderMeals(string id)
        {
            var meals = context.Meals.Where(ss => ss.ProviderId == id).ToList();

            return PartialView(meals);
        }



        public ActionResult Create(string id)
        {
            ViewBag.providerid = context.Providers.FirstOrDefault(ss => ss.ProviderId == id);
            var catiegories = context.Categories.Select(ww => ww.CategoryId);
            ViewBag.CategoriesList = new SelectList(context.Categories, " CategoryId", "Name", catiegories);
            Meal meal = new Meal() { ProviderId = id };
            return PartialView(meal);
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMeal(Meal meal, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                Image.SaveAs(Server.MapPath("~/Resources/" + Image.FileName));
                meal.Image = "~/Resources/" + Image.FileName;
                context.Entry(meal).State = EntityState.Added;

                context.SaveChanges();
                return PartialView("ProviderMeals", context.Meals.Where(ss => ss.ProviderId == meal.ProviderId).ToList());
            }

            return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
        }




















        // GET: Meals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = await context.Meals.FindAsync(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return PartialView(meal);
        }


        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // GET: Providers/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meal meal = context.Meals.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriesList = new SelectList(context.Categories, " CategoryId", "Name", meal.CategoryId);
            return PartialView(meal);
        }







        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MealId,Name,Price,Description,ExecutionTime,Available,CategoryId,ProviderId,Image")] Meal meal, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {


                Image.SaveAs(Server.MapPath("~/Resources/" + Image.FileName));
                meal.Image = "~/Resources/" + Image.FileName;
                context.Entry(meal).State = EntityState.Modified;
                context.SaveChanges();
                return PartialView("ProviderMeals", context.Meals.Where(ss => ss.ProviderId == meal.ProviderId).ToList());
            }
            return View(meal);
        }


























        // GET: Meals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var meal = context.Meals.FirstOrDefault(ss => ss.MealId == id);
            context.Entry(meal).State = System.Data.Entity.EntityState.Deleted;
            var meals = context.Meals.Where(ss => ss.ProviderId == meal.ProviderId).ToList();
            context.SaveChanges();

            return PartialView("ProviderMeals", meals.ToList());
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}
