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
    public class ProductController : Controller
    {
        private Membersql db = new Membersql();

        #region 6.取得類別GET
        public ActionResult GetCategory()
        {
            return Content(JsonConvert.SerializeObject(db.ProductCategoryList.Select(x => new
            {
                PCid=x.Id,
                x.PCName
            })));
        }
        #endregion

        #region 2.取得商品列表GET
        public ActionResult GetProduct(int? PCid)
        {
            var result = db.ProductLists.Select(x => new
            {
                Pid = x.Id,
                Name = x.Name,
                Price = x.Price,
                PCid = x.PCid,
                //Img = db.ProductImg.Where(w => w.Pid == x.Id).Select(w=>w.Pimg)這樣開兩次資料庫效能差
                Img = x.ProductImg.Select(w=>w.Pimg)
            });
            if (PCid != null)
            {
                result = result.Where(x => x.PCid == PCid);
            }
            return Content(JsonConvert.SerializeObject(result));
        }
        #endregion

        #region 3.取得單品細節GET

        public ActionResult GetProductDetail(int id)
        {
            var result = db.ProductLists.Where(x => x.Id == id).Select(x => new
            {
                Pid = x.Id,
                Name = x.Name,
                Price = x.Price,
                PCid = x.PCid,
                Img = db.ProductImg.Where(w => w.Pid == x.Id).Select(w => w.Pimg),
                Description= x.Description,
                Sides1 = x.Sides1,
                Sides2 = x.Sides2,
                Sides3 = x.Sides3,
                Sides4 = x.Sides4
            });
            //var result = db.ProductLists.Include(x=>x.ProductImg).Where(x => x.Id == id);
            return Content(JsonConvert.SerializeObject(result));
            //return Content(JsonConvert.SerializeObject(db.ProductLists.Where(x => x.Id == id).Select(x=>new {x.Id,x.Name,x.Img,x.Description,x.Price,x.Sides1,x.Sides2,x.Sides3,x.Sides4})));
        }

        #endregion

        // GET: ProductLists
        public ActionResult Index()
        {
            var productLists = db.ProductLists.Include(p => p.ProductCategory);
            return View(productLists.ToList());
        }

        // GET: ProductLists/Details/5
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

        // GET: ProductLists/Create
        public ActionResult Create()
        {
            ViewBag.PCid = new SelectList(db.ProductCategoryList, "Id", "PCName");
            return View();
        }

        // POST: ProductLists/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PCid,Name,Price,Img,Sides1,Sides2,Sides3,Sides4")] ProductList productList)
        {
            if (ModelState.IsValid)
            {
                db.ProductLists.Add(productList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PCid = new SelectList(db.ProductCategoryList, "Id", "PCName", productList.PCid);
            return View(productList);
        }

        // GET: ProductLists/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.PCid = new SelectList(db.ProductCategoryList, "Id", "PCName", productList.PCid);
            return View(productList);
        }

        // POST: ProductLists/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PCid,Name,Price,Img,Sides1,Sides2,Sides3,Sides4")] ProductList productList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PCid = new SelectList(db.ProductCategoryList, "Id", "PCName", productList.PCid);
            return View(productList);
        }

        // GET: ProductLists/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: ProductLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductList productList = db.ProductLists.Find(id);
            db.ProductLists.Remove(productList);
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
