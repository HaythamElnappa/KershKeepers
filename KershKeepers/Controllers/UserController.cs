using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using KershKeepers.Models;
using KershKeepers.Models.EFModels;

namespace KershKeepers.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private ApplicationDbContext db;
        public UserController()
        {
            db = new ApplicationDbContext();
        }
        
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllUsers()
        {
            return PartialView(db.Users.ToList());
        }
        
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            user.IsDeleted = !user.IsDeleted;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GetAllUsers");
        }

    }
}