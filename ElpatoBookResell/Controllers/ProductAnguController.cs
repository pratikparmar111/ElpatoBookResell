using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MVCManukauTech.Models;
using Newtonsoft.Json;

namespace MVCManukauTech.Controllers
{
    public class ProductAnguController : ApiController
    {
        [Route("api/AjaxAPI/AjaxMethod")]
        [HttpPost]
        public List<Product> AjaxMethod(Product Pro)
        {
            List<Product> Product1;

            elpatobookresellEntities db = new elpatobookresellEntities();

            Product1= db.Products.Where(p => p.BookName.Contains(Pro.BookName)).ToList();

           
            return Product1;
        }
    }
}
