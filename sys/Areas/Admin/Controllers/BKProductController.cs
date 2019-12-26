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
    public class BKProductController : Controller
    {
        private Membersql db = new Membersql();
        private const int PageSize = 10;
        // GET: Admin/BKProduct
        public ActionResult Index(int? page)
        {
            ViewBag.CatagoryId = new SelectList(db.ProductCategoryList, "Id", "PCName");
            if (!page.HasValue)
            {
                page = 0;
            }
            else
            {
                page--;//ToPagedList的pageIndex預設第一頁是0,第二頁是1，所以要-1才是真的頁面
            }
            var productLists = db.ProductLists.Include(p => p.ProductCategory).Include(w=>w.ProductImg);
            return View(productLists.ToList().ToPagedList((int)page, PageSize));
        }
        [HttpPost]
        public ActionResult Index(int? CatagoryId, int? page)
        {
            try
            {
                if (!page.HasValue)
                {
                    page = 0;
                }
                else
                {
                    page--;//ToPagedList的pageIndex預設第一頁是0,第二頁是1，所以要-1才是真的頁面
                }
                var productLists = db.ProductLists.Include(p => p.ProductCategory).Include(w => w.ProductImg);
                if (CatagoryId != null)
                {
                    productLists = productLists.Where(x => x.ProductCategory.Id == CatagoryId);
                }
                ViewBag.CatagoryId = new SelectList(db.ProductCategoryList, "Id", "PCName");
                return View(productLists.ToList().ToPagedList((int)page, PageSize));
            }
            catch
            {
                return View();
            }
            
        }
        // GET: Admin/BKProduct/Create
        public ActionResult Create()
        {
            ViewBag.PCid = new SelectList(db.ProductCategoryList, "Id", "PCName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PCid,Name,Description,Price,Sides1,Sides2,Sides3,Sides4")] ProductList productList)
        {
            if (ModelState.IsValid)
            {
                db.ProductLists.Add(productList);
                db.SaveChanges();
                return RedirectToAction("Edit","BKProductImgs",new { id=productList.Id });
            }

            ViewBag.PCid = new SelectList(db.ProductCategoryList, "Id", "PCName", productList.PCid);
            return View(productList);
        }
        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductList productList = db.ProductLists.Find(id);
            if (productList == null)
            {
                return HttpNotFound();
            }
            return View(productList);
        }
        // GET: Admin/BKProduct/Edit/5
        public ActionResult Edit(int? id,int Page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductList productList = db.ProductLists.Find(id);
            ViewBag.id = id;
            ViewBag.Page = Page;
            if (productList == null)
            {
                return HttpNotFound();
            }
            ViewBag.PCid = new SelectList(db.ProductCategoryList, "Id", "PCName", productList.PCid);
            return View(productList);
        }

        // POST: Admin/BKProduct/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PCid,Name,Description,Price,Sides1,Sides2,Sides3,Sides4")] ProductList productList, int Page)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PCid = new SelectList(db.ProductCategoryList, "Id", "PCName", productList.PCid);
            ViewBag.id = productList.Id;
            ViewBag.Page = Page;
            return View(productList);
        }

        // GET: Admin/BKProduct/Delete/5
        public ActionResult Delete(int? id,int Page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductList productList = db.ProductLists.Find(id);
            ViewBag.Page = Page;
            if (productList == null)
            {
                return HttpNotFound();
            }
            return View(productList);
        }

        // POST: Admin/BKProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int Page)
        {
            ProductList productList = db.ProductLists.Find(id);
            db.ProductLists.Remove(productList);
            db.SaveChanges();
            ViewBag.Page = Page;
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
