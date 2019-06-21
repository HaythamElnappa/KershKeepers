using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KershKeepers.Models;
using KershKeepers.Models.EFModels;

namespace KershKeepers.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db;

        public CategoriesController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Categories
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getAll()
        {
            return PartialView(db.Categories.ToList());
        }


        // GET: Categories/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = db.Categories.Find(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return PartialView(category);
        //}

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,Name,Image,IsDeleted")] Category category,HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                uploadImage.SaveAs(Server.MapPath("~/Resources/Categories/"+uploadImage.FileName));
                category.Image = "~/Resources/Categories/" + uploadImage.FileName;
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,Name,Image,IsDeleted")] Category category, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {

                Image.SaveAs(Server.MapPath("~/Resources/Categories/" + Image.FileName));
                category.Image = "~/Resources/Categories/" + Image.FileName;
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(category);
        }

        //GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            else
            {
                db.Entry(category).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("getAll");
            }
        }

        ////POST: Categories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int? id)
        //{
        //    if (id == null)
        //          {
        //       return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //           }                                                                               
        //     Category category = db.Categories.Find(id);
        //    db.Categories.Remove(category);
        //    db.SaveChanges();
        //    return View(category);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
