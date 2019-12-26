using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using sys.Models;

namespace sys.Controllers
{
    public class AreasController : Controller
    {
        private Membersql db = new Membersql();

        //GET 
        public ActionResult County()
        {
            return Content(JsonConvert.SerializeObject(db.City.Select(x=>x.County)));
        }
        //GET
        public ActionResult Town(string county)
        {
            return Content(JsonConvert.SerializeObject(db.Areas.Where(x=>x.County==county).Select(x=>x.Town).ToList()));
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
