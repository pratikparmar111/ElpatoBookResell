using MVCManukauTech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVCManukauTech.Models.VIewModel;
using System.Net;

namespace MVCManukauTech.Controllers
{
    // To only admin user allow on page 
    //pratik 26-9
    [Authorize(Roles = "Admin")]
    public class pageckeditorController : Controller
    {

        private elpatobookresellEntities db = new elpatobookresellEntities();
        // GET: pageckeditor
        public ActionResult Index()
        {
            return View(db.pageckeditors.ToList());
        }


        // GET: pageckeditors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pageckeditor pageckeditor = db.pageckeditors.Find(id);
            if (pageckeditor == null)
            {
                return HttpNotFound();
            }
            return View(pageckeditor);
        }

        // GET: pageckeditor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: pageckeditor/Create
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Create(pageckeditorViewModel pageckeditorViewModel)
        {
            try
            {
                // TODO: Add insert logic here

                if (ModelState.IsValid)
                {
                    pageckeditor pageckeditor1 = new pageckeditor();
                    pageckeditor1.htmlvalue = pageckeditorViewModel.htmlvalue;
                    pageckeditor1.pagename = pageckeditorViewModel.pagename;
                    db.pageckeditors.Add(pageckeditor1);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: pageckeditor/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pageckeditor pageckeditor = db.pageckeditors.Find(id);
            if (pageckeditor == null)
            {
                return HttpNotFound();
            }
            return View(pageckeditor);
        }

        // POST: pageckeditor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, pageckeditorViewModel pageckeditorViewModels)
        {
            try
            {
                // TODO: Add update logic here

                pageckeditor pageckeditor1 = new pageckeditor();
                pageckeditor1.id = id;
                pageckeditor1.htmlvalue = pageckeditorViewModels.htmlvalue;
                pageckeditor1.pagename = pageckeditorViewModels.pagename;
                db.Entry(pageckeditor1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: pageckeditors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            pageckeditor pageckeditor = db.pageckeditors.Find(id);
            if (pageckeditor == null)
            {
                return HttpNotFound();
            }
            return View(pageckeditor);
        }

        // POST: pageckeditors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            pageckeditor pageckeditor = db.pageckeditors.Find(id);
            db.pageckeditors.Remove(pageckeditor);
            db.SaveChanges();
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
