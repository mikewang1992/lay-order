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
using MvcPaging;

namespace sys.Controllers
{
    public class KitchenController : Controller
    {
        private Membersql db = new Membersql();
        #region 35.顯示待處理訂單GET
        public ActionResult ShowOrderList(string type, string status, int page)
        {
            if (Session["EmployeeID"] == null)
            {
                return Content("未登入");
            }
            int PageSize = 4;
            page = page - 1;

            var result = db.Orders.Select(x => new
            {
                //訂單
                orderid = x.Id,
                isTable = x.Account.IsTable,
                customer = x.Account.Name,
                ordertime = x.OrderTime,
                gettime = x.GetTime,
                status = x.Status,
                totalQty = x.OrderDetails.Sum(w => w.Qty),
                //細項
                product = x.OrderDetails.Where(w => w.Oid == x.Id).Select(w => new
                {
                    Id = w.Id,
                    ProductName = w.ProductList.Name,
                    option = w.Options,
                    Qty = w.Qty,
                    status = w.Status
                })
            });

            bool isTable = false;
            if (type == "forhere")
            {
                isTable = true;
            }

            if (!string.IsNullOrEmpty(type))
            {
                result = result.Where(x => x.isTable == isTable);
                if (type == "big")
                {
                    int qty = db.CompanySet.OrderByDescending(z => z.Id).FirstOrDefault().BigQty;
                    result = result.Where(x => x.totalQty > qty);
                }
            }
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "done")
                {
                    result = result.Where(x => x.status == "done" || x.status == "finish" || x.status == "paid").OrderByDescending(x => x.gettime);
                }
                else
                {
                    result = result.Where(x => x.status == status);
                }
            }
            else
            {
                result = result.Where(x => x.status == "prepare" || x.status == "ready").OrderByDescending(x => x.gettime);
            }
            var finalResult = result.OrderBy(x => x.gettime).ToPagedList(page, PageSize);
            return Content(JsonConvert.SerializeObject(finalResult));
        }

        #endregion

        #region 36訂單完成GET
        public ActionResult CompleteOrder(int id)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                Order order = db.Orders.Find(id);
                if (order.Status != "done")
                {
                    order.Status = "finish";
                }
                List<OrderDetail> orderDetail = db.OrderDetails.Where(x => x.Oid == id).ToList();
                foreach (var item in orderDetail)
                {
                    if (item.Status == "prepare")
                    {
                        item.Status = "ready";
                    }
                }
                db.SaveChanges();
                return Content("success");
            }
            catch
            {
                return Content("fail");
            }
        }
        #endregion

        #region 37單品完成GET
        public ActionResult CompleteOrderItem(int Oid, int id)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                OrderDetail orderDetail = db.OrderDetails.Find(id);
                orderDetail.Status = "ready";
                Order order = db.Orders.Find(Oid);
                if (orderDetail.Status != "done")
                {
                    order.Status = "ready";
                }
                db.SaveChanges();
                return Content("success");
            }
            catch
            {
                return Content("fail");
            }
        }
        #endregion

        #region 39.總頁數GET
        public ActionResult TotalPage(string type, string status)
        {
            if (Session["EmployeeID"] == null)
            {
                return Content("未登入");
            }

            var result = db.Orders.Select(x => new
            {
                isTable = x.Account.IsTable,
                gettime = x.GetTime,
                status = x.Status,
                totalQty = x.OrderDetails.Sum(w => w.Qty),
            });

            bool isTable = false;
            if (type == "forhere")
            {
                isTable = true;
            }

            if (!string.IsNullOrEmpty(type))
            {
                result = result.Where(x => x.isTable == isTable);
                if (type == "big")
                {
                    int qty = db.CompanySet.OrderByDescending(z => z.Id).FirstOrDefault().BigQty;
                    result = result.Where(x => x.totalQty > qty);
                }
            }
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "done")
                {
                    result = result.Where(x => x.status == "done" || x.status == "finish").OrderByDescending(x => x.gettime);
                }
                result = result.Where(x => x.status == status);
            }
            else
            {
                result = result.Where(x => x.status == "prepare" || x.status == "ready").OrderByDescending(x => x.gettime);
            }
            int page = result.Count();
            if (page % 4 == 0)
            {
                page = page / 4;
            }
            page = (page / 4) + 1;
            return Content(page.ToString());
        }
        #endregion

        #region 46.取消待出餐
        public ActionResult BackToPrepare(int Pid)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                var orderDetail = db.OrderDetails.Find(Pid);
                orderDetail.Status = "prepare";
                db.SaveChanges();
                return Content("success");
            }
            catch
            {
                return Content("fail");
            }
        }


        #endregion

        // GET: Kitchen
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.order).Include(o => o.ProductList);
            return View(orderDetails.ToList());
        }

        // GET: Kitchen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // GET: Kitchen/Create
        public ActionResult Create()
        {
            ViewBag.Oid = new SelectList(db.Orders, "Id", "Status");
            ViewBag.Pid = new SelectList(db.ProductLists, "Id", "Name");
            return View();
        }

        // POST: Kitchen/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Oid,Pid,Options,Qty,Price,Status")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Oid = new SelectList(db.Orders, "Id", "Status", orderDetail.Oid);
            ViewBag.Pid = new SelectList(db.ProductLists, "Id", "Name", orderDetail.Pid);
            return View(orderDetail);
        }

        // GET: Kitchen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.Oid = new SelectList(db.Orders, "Id", "Status", orderDetail.Oid);
            ViewBag.Pid = new SelectList(db.ProductLists, "Id", "Name", orderDetail.Pid);
            return View(orderDetail);
        }

        // POST: Kitchen/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Oid,Pid,Options,Qty,Price,Status")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Oid = new SelectList(db.Orders, "Id", "Status", orderDetail.Oid);
            ViewBag.Pid = new SelectList(db.ProductLists, "Id", "Name", orderDetail.Pid);
            return View(orderDetail);
        }

        // GET: Kitchen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            if (orderDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderDetail);
        }

        // POST: Kitchen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Find(id);
            db.OrderDetails.Remove(orderDetail);
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
