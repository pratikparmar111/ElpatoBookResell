using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCManukauTech.Models;
using System.Web.Mvc.Html;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace MVCManukauTech.Controllers
{
    ////only registed user and admin can see this page //pratik 
    [Authorize]

    public class ProductsController : Controller
    { 
        private elpatobookresellEntities db = new elpatobookresellEntities();
       
        
        // GET: Products
        public ActionResult Index(string searchString)
        {
            //For allowing which user added the product only allow them to see it and edit it 
            string LoginUserid = User.Identity.GetUserId();
            if (string.IsNullOrEmpty(searchString))
            {              
                var products = db.Products.Include(p => p.Category).Where(p =>p.userID == LoginUserid);
                return View(products.ToList());

            }
            else
            {
                string sql = "SELECT * FROM Products WHERE BookName LIKE @p0 or ISBNNum LIKE @p0 and userID=" + LoginUserid;
                searchString = "%" + searchString + "%";

                List<Product> Product = db.Products.SqlQuery(sql, searchString).ToList();

                return View(Product);
            }
        }       
        //ajex seach for autocompelte
        public string ajaxsearch(string SearchString)
        {
            string LoginUserid = User.Identity.GetUserId();
            SearchString = "%" + SearchString + "%";
            string sql = "SELECT [BookName] FROM products WHERE( BookName LIKE @p0 or ISBNNum LIKE @p0 ) and  userID=" + "'"+ LoginUserid +"'";
            List<string> products = db.Database.SqlQuery<string>(sql, SearchString).ToList();
            string json =JsonConvert.SerializeObject(products);
            return json;
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,CategoryId,BookName,ImageFileName,UnitCost,Description,IsDownload,DownloadFileName,ISBNNum,userID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", product.userID);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", product.userID);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,CategoryId,BookName,ImageFileName,UnitCost,Description,IsDownload,DownloadFileName,ISBNNum,userID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.AspNetUsers, "Id", "Email", product.userID);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
