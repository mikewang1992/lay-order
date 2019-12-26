using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sys.Models;
using MvcPaging;
using sys.Filters;

namespace sys.Areas.Admin.Controllers
{
    [PermissionFilter]
    [Authorize]
    public class BKAccountsController : Controller
    {
        private Membersql db = new Membersql();
        private const int PageSize = 10;
        // GET: Admin/BKAccounts
        public ActionResult Index(int? page)
        {
            if (!page.HasValue)
            {
                page = 0;
            }
            else
            {
                page--;//ToPagedList的pageIndex預設第一頁是0,第二頁是1，所以要-1才是真的頁面
            }
            var result = db.Accounts.Where(x => x.IsTable == false).OrderByDescending(x => x.Id).ToList();
            //ViewBag.County = new SelectList(db.Areas.Distinct(), "County", "County");
            //ViewBag.Town = new SelectList(db.Areas, "Town", "Town");
            return View(result.ToPagedList((int)page, PageSize));
        }
        [HttpPost]
        public ActionResult Index(string Tel,string Name, int? page)
        {
            if (!page.HasValue)
            {
                page = 0;
            }
            else
            {
                page--;
            }
            var result = db.Accounts.Where(x => x.IsTable == false).OrderByDescending(x => x.Id).AsQueryable();
            if (!string.IsNullOrEmpty(Tel))
            {
                result = result.Where(x => x.Tel.Contains(Tel));
            }
            if (!string.IsNullOrEmpty(Name))
            {
                result = result.Where(x => x.Name.Contains(Name));
            }
            return View(result.ToList().ToPagedList((int)page, PageSize));
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            if (ModelState.IsValid)
            {
                Account account = db.Accounts.Find(id);
                account.Sent = 0;
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // POST: Admin/BKAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
