using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using sys.Filters;
using sys.Models;

namespace sys.Areas.Admin.Controllers
{
    [PermissionFilter]
    [Authorize]
    public class BKVouchersController : Controller
    {
        private Membersql db = new Membersql();
        private const int PageSize = 10;
        // GET: Admin/Vouchers
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
            return View(db.Vouchers.ToList().ToPagedList((int)page, PageSize));
        }

        // GET: Admin/Vouchers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // GET: Admin/Vouchers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Vouchers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Content,StartTime,EndTime")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                if (voucher.StartTime > voucher.EndTime)
                {
                    ViewBag.Message = "起始日期不能大於結束日期";
                    ViewBag.CTitle = voucher.Title;
                    ViewBag.Content = voucher.Content;
                    return View(voucher);
                }
                db.Vouchers.Add(voucher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(voucher);
        }

        // GET: Admin/Vouchers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            ViewBag.VTitle = voucher.Title;
            ViewBag.Content = voucher.Content;
            ViewBag.StartTime = voucher.StartTime.ToString("yyyy-MM-dd");
            ViewBag.EndTime = voucher.EndTime.ToString("yyyy-MM-dd");
            return View(voucher);
        }

        // POST: Admin/Vouchers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Content,StartTime,EndTime")] Voucher voucher)
        {
            if (ModelState.IsValid)
            {
                if (voucher.StartTime > voucher.EndTime)
                {
                    ViewBag.Message = "起始日期不能大於結束日期";
                    ViewBag.VTitle = voucher.Title;
                    ViewBag.Content = voucher.Content;
                    ViewBag.StartTime = voucher.StartTime.ToString("yyyy-MM-dd");
                    ViewBag.EndTime = voucher.EndTime.ToString("yyyy-MM-dd");
                    return View(voucher);
                }
                db.Entry(voucher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(voucher);
        }

        // GET: Admin/Vouchers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voucher voucher = db.Vouchers.Find(id);
            if (voucher == null)
            {
                return HttpNotFound();
            }
            return View(voucher);
        }

        // POST: Admin/Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Voucher voucher = db.Vouchers.Find(id);
            db.Vouchers.Remove(voucher);
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
