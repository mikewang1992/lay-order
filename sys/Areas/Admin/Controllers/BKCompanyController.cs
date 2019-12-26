using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sys.Areas.Admin.ViewModel;
using sys.Filters;
using sys.Models;

namespace sys.Areas.Admin.ViewModel
{
    [PermissionFilter]
    [Authorize]
    public class BKCompanyController : Controller
    {
        private Membersql db = new Membersql();
        //GET
        public ActionResult Index()
        {
            BKCompanyViewModel company = new BKCompanyViewModel();
            Company original = db.CompanySet.Find(1);
            IEnumerable<holiday> holiday = db.holiday.OrderByDescending(x=>x.StartTime).ToList().Take(5);
            ViewBag.holiday = holiday;
            company.Tel = original.Tel;
            company.BigQty = original.BigQty;
            company.StartTime = original.StartTime;
            company.EndTime = original.EndTime;
            company.PrepareTime = original.PrepareTime;
            //company.Holidays = holiday;
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company company)
        {
            if (ModelState.IsValid)
            {
                company.Id = 1;
                db.Entry(company).State = EntityState.Modified;
                //Company oldCompany = db.CompanySet.Find(1);
                //oldCompany.Tel = company.Tel;
                //oldCompany.BigQty = company.BigQty;
                //oldCompany.PrepareTime = company.PrepareTime;
                //oldCompany.StartTime = company.StartTime;
                //oldCompany.EndTime = company.EndTime;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Create(holiday holiday)
        {
            if (ModelState.IsValid)
            {
                if (holiday.EndTime<holiday.StartTime)
                {
                    ViewBag.Alert = "起始時間不得小於結束時間";
                    return RedirectToAction("Index", "BKCompany");
                }
                //holiday newHoliday =new holiday();
                //newHoliday.StartTime = holiday.HolidayStartTime;
                //newHoliday.EndTime = holiday.HolidayEndTime;
                db.holiday.Add(holiday);
                db.SaveChanges();
                return RedirectToAction("Index", "BKCompany");
            }

            ViewBag.Alert = "時間不可以為空值";
            return RedirectToAction("Index", "BKCompany");
        }
        [HttpPost]
        public ActionResult Delete(holiday newholiday)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            int id = newholiday.Id;
            holiday holiday = db.holiday.Find(id);
            if (holiday == null)
            {
                return HttpNotFound();
            }
            db.holiday.Remove(holiday);
            db.SaveChanges();
            return RedirectToAction("Index", "BKCompany");
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
