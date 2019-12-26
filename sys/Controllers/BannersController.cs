using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using sys.Models;

namespace sys.Controllers
{
    public class BannersController : Controller
    {
        private Membersql db = new Membersql();

        // GET: Banners
        public ActionResult GetBanners()
        {
            DateTime today = DateTime.UtcNow.AddHours(23);
            return Content(JsonConvert.SerializeObject(db.Banners.Where(x=>x.EndTime>today &&x.StartTime<today).Select(x=>x.Img)));
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
