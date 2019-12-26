using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MvcPaging;
using Newtonsoft.Json;
using sys.Models;

namespace sys.Controllers
{
    public class OrderController : Controller
    {
        private Membersql db = new Membersql();


        // GET: Order
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.order).Include(o => o.ProductList);
            return View(orderDetails.ToList());
        }

        // GET: Order/Details/5
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

        // GET: Order/Create
        public ActionResult Create()
        {
            ViewBag.Oid = new SelectList(db.Orders, "Id", "Status");
            ViewBag.Pid = new SelectList(db.ProductLists, "Id", "Name");
            return View();
        }

        //訂單頁面
        #region 9.確認點餐POST
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create(OrderContent[] orderDetail)
        {
            try
            {
                //if (bool.Parse(Session["verification"].ToString()) != true)
                //{
                //    return Content("機器人來襲");
                //}
                if (orderDetail == null)
                {
                    return Content("請選擇訂購商品");
                }

                //建立總表
                Order order = new Order();
                order.Cid = Convert.ToInt32(Session["Id"]);
                //DateTime orderTime = orderDetail[0].time != null
                //    ? Convert.ToDateTime(orderDetail[0].time).AddHours(23)
                //    : DateTime.UtcNow.AddHours(23);
                //order.OrderTime = orderTime;

                order.OrderTime = Convert.ToDateTime(orderDetail[0].orderTime);
                //int preTime = db.CompanySet.OrderByDescending(x => x.Id).FirstOrDefault().PrepareTime;
                order.GetTime = Convert.ToDateTime(orderDetail[0].getTime);
                order.Status = "prepare";
                db.Orders.Add(order);
                db.SaveChanges();
                try
                {
                    //建立細項
                    foreach (var item in orderDetail)
                    {
                        OrderDetail orderItem = new OrderDetail();
                        orderItem.Oid = order.Id;
                        orderItem.Pid = item.Pid;
                        orderItem.Options = item.Options;
                        orderItem.Qty = item.Qty;
                        orderItem.Price = db.ProductLists.Where(x => x.Id == item.Pid).FirstOrDefault().Price;
                        orderItem.Status = "prepare";
                        db.OrderDetails.Add(orderItem);
                    }
                }
                catch
                {
                    db.Orders.Remove(order);
                    db.SaveChanges();
                    return Content("fail");
                }
                db.SaveChanges();
                return Content(order.Id.ToString());
            }
            catch
            {
                return Content("fail");
            }
        }
        #endregion

        #region 訂單成功-取得時間/總額/顧客資訊GET

        public ActionResult OrderSuccess(int id)
        {
            return Content(JsonConvert.SerializeObject(db.Orders.Where(x => x.Id == id).Select(x => new
            {
                time = x.GetTime,
                name = x.Account.Name,
                tel = x.Account.Tel,
                total = x.OrderDetails.Sum(w => w.Qty * w.Price)
            })));
        }
        #endregion
        //會員中心

        #region 顯示訂單狀態GET

        public ActionResult ShowOrderStatus()
        {
            int id = Convert.ToInt32(Session["Id"]);
            var result = db.Orders.Where(x => x.Cid == id).Select(x => new
            {
                id = x.Id,
                status = x.Status,
                time = x.GetTime,
                total = x.OrderDetails.Sum(w => w.Price * w.Qty)
            }).OrderByDescending(x => x.status == "finish").ThenByDescending(x => x.status == "done").ThenByDescending(x => x.status == "ready").ThenByDescending(x => x.status == "prepare").ThenByDescending(x => x.time);
            return Content(JsonConvert.SerializeObject(result));
        }
        #endregion

        #region 13.顯示訂單詳情-明細GET
        public ActionResult ShowOrderDetail(int id)
        {
            var result = db.OrderDetails.Where(x => x.Oid == id).Select(x => new
            {
                pid = x.Id,
                name = x.ProductList.Name,
                img = x.ProductList.ProductImg.Select(w => w.Pimg).Take(1),
                options = x.Options,
                Qty = x.Qty,
                subtotal = x.Qty * x.Price,
                status = x.Status
            });
            return Content(JsonConvert.SerializeObject(result));
        }
        #endregion

        #region 14.顯示訂單詳情-總和GET

        public ActionResult ShowOrderSummary(int id)
        {
            var result = db.Orders.Where(x => x.Id == id).Select(x => new
            {
                name = x.Account.Name,
                tel = x.Account.Tel,
                ordertime = x.OrderTime,
                gettime = x.GetTime,
                totalQty = x.OrderDetails.Sum(w => w.Qty),
                totalAmount = x.OrderDetails.Sum(w => w.Qty * w.Price)
            });
            return Content(JsonConvert.SerializeObject(result));
        }

        #endregion



        // GET: Order/Edit/5
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

        // POST: Order/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Oid,Pid,Options,Qty,Status")] OrderDetail orderDetail)
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

        // GET: Order/Delete/5
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

        // POST: Order/Delete/5
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
