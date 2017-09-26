using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCManukauTech.Models.VIewModel
{
    public class pageckeditorViewModel
    {
        public int id { get; set; }

        public string pagename { get; set; }
        [AllowHtml]
        public string htmlvalue { get; set; }
    }
}