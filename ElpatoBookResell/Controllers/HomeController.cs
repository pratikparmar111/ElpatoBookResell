using MVCManukauTech.Models;
using System.Web.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace MVCManukauTech.Controllers
{
    public class HomeController : Controller
    {
        private elpatobookresellEntities db = new elpatobookresellEntities();
        public ActionResult Index()
        {
            // to show index page from databasse its id is 1           
          //hello
            ViewBag.Message_Catalog = db.pageckeditors.Find(1).htmlvalue;
            ViewBag.Message_Products = db.pageckeditors.Find(8).htmlvalue;
            ViewBag.Message_Prime = db.pageckeditors.Find(12).htmlvalue;     

            // to check prime 
            //for checking prime membership 

            List<premiumUser> premiumUsersCheck = db.premiumUsers.ToList();
            premiumUsersCheck = premiumUsersCheck.Where(p => p.userID == User.Identity.GetUserId()).OrderByDescending(p => p.endDate).ToList();
            // List<premiumUser> premiumUsersCheck =  db.premiumUsers.Where(p => p.userID == User.Identity.GetUserId()).ToList();
            if (premiumUsersCheck.Count() != 0)
            {
                if (premiumUsersCheck[0].endDate < DateTime.Now)
                {
                    Session["primeStatus"] = "ExpiredPrime";
                    if (Session["primeStatusShown"] != null)
                    {
                        if (Session["primeStatusShown"].ToString() != "Yes")
                        {
                            Session["primeStatusShown"] = "No";
                        }
                    }
                    else
                    {
                        Session["primeStatusShown"] = "No";
                    }
                }
                else
                {
                    Session["primeStatus"] = "Y";
                    //ViewBag.Message_Prime = "Y";
                }
            }
            return View();
        }

        // [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";
            // to send data to contact page
            pageckeditor pageckeditor = db.pageckeditors.Find(7);

            var temp = pageckeditor.htmlvalue;
            ViewBag.Message1 = temp;
            return View();
        }
    }
}
