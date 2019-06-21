using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using KershKeepers.Models;
using KershKeepers.Models.EFModels;

namespace KershKeepers.Controllers
{
    public class ProvidersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string email,string  password)
        {
            //string criptPass = Crypto.VerifyHashedPassword()
            var provider = db.Providers.FirstOrDefault(ww => ww.Email == email);
            if (provider == null)
            {
                ViewBag.Message = "Invalid Sign In";
                return View();
            }
            else
            {
                if (Crypto.VerifyHashedPassword(provider.Password, password))
                {
                    if (!provider.IsActivated)
                    {
                        return HttpNotFound();
                    }
                    Session.Add("ProviderId",provider.ProviderId);
                    return RedirectToAction("ProviderHome", new {id = provider.ProviderId});
                }
                else
                {
                    ViewBag.Message = "Invalid Sign In";
                    return View();
                }
            }
            
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Provider provider)
        {
            string ProviderId = Guid.NewGuid().ToString("N");
            provider.ProviderId = ProviderId;
            provider.IsActivated = false;
            provider.Password = Crypto.HashPassword(provider.Password);
            db.Providers.Add(provider);
            db.SaveChanges();
            return RedirectToAction("LogIn");
        }


        // GET: Providers

        public ActionResult ProviderHome(string id)
        {
            var provider =  db.Providers.FirstOrDefault(ww => ww.ProviderId == id);

            return View(provider);
        }

        // GET: Providers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                Provider provider = db.Providers.FirstOrDefault(ss => ss.ProviderId == id);
                if (provider == null)
                {
                    return HttpNotFound();
                }
                else {
                    return PartialView(provider);
                }
                
            }
               
        }

        // GET: Providers/Create
       

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProviderId,Name,Email,Password,Phone,Address,Image,Type,WorkStartTime,WorkEndTime,RegisterDate,IsDeleted")] Provider provider, HttpPostedFileBase uploadImage)
        {
            if (ModelState.IsValid)
            {
                uploadImage.SaveAs(Server.MapPath("~/Resources/" + uploadImage.FileName));
                provider.Image = "~/Resources/" + uploadImage.FileName;
                
                db.Providers.Add(provider);
                 db.SaveChanges();
                return RedirectToAction("ProviderHome", new { provider.ProviderId});
            }

            return View(provider);
        }

        // GET: Providers/Edit/5
        public  ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider =  db.Providers.Find(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return PartialView(provider);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProviderId,Name,Email,Password,Phone,Address,Image,Type,WorkStartTime,WorkEndTime,RegisterDate,IsDeleted")] Provider provider,HttpPostedFileBase Image)
        {
          

                Image.SaveAs(Server.MapPath("~/Resources/" + Image.FileName));
                provider.Image = "~/Resources/" + Image.FileName;
                db.Entry(provider).State = EntityState.Modified;
                 db.SaveChanges();
                return PartialView("Details",provider);
            
       
        }

        // GET: Providers/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Provider provider = await db.Providers.FindAsync(id);
            if (provider == null)
            {
                return HttpNotFound();
            }
            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Provider provider = await db.Providers.FindAsync(id);
            db.Providers.Remove(provider);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
