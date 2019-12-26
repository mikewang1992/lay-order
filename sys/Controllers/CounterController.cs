using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using Newtonsoft.Json;
using sys.Models;

namespace sys.Controllers
{
    public class CounterController : Controller
    {
        private Membersql db = new Membersql();

        //櫃台
        #region 30.顯示訂單總表GET
        public ActionResult ShowOrderList(string type, string status, int page)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                int PageSize = 9;
                page = page - 1;
                var result = db.Orders.Select(x => new
                {
                    Orderid = x.Id,
                    isTable = x.Account.IsTable,
                    customer = x.Account.Name,
                    tel = x.Account.Tel,
                    ordertime = x.OrderTime,
                    gettime = x.GetTime,
                    total = x.OrderDetails.Sum(w => w.Price * w.Qty),
                    status = x.Status
                });

                bool isTable = false;
                if (type == "forhere")
                {
                    isTable = true;
                }

                if (!string.IsNullOrEmpty(type))
                {
                    result = result.Where(x => x.isTable == isTable);
                }
                if (!string.IsNullOrEmpty(status))
                {
                    if (status == "ready")
                    {
                        result = result.Where(x => x.status == "ready" || x.status == "finish");
                    }
                    result = result.Where(x => x.status == status);
                }
                else
                {
                    result = result.Where(x => x.status != "cancel" && x.status != "paid");
                }

                var finalResult = result.OrderBy(x => x.gettime).ToPagedList(page, PageSize);
                return Content(JsonConvert.SerializeObject(finalResult));
            }
            catch
            {
                return Content("fail");
            }
        }

        #endregion

        #region 33取消訂單GET

        public ActionResult CancelOrder(int id)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                Order order = db.Orders.Find(id);
                order.Status = "cancel";
                db.SaveChanges();
                return Content("success");
            }
            catch
            {
                return Content("fail");
            }
        }

        #endregion

        #region 34結帳GET

        public ActionResult CheckOrder(int id)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                Order order = db.Orders.Find(id);
                order.Status = "paid";
                db.SaveChanges();
                return Content("success");
            }
            catch
            {
                return Content("fail");
            }
        }

        #endregion

        #region 32.單品送餐完畢
        public ActionResult ItemDelivered(int Oid, int id)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                OrderDetail orderDetail = db.OrderDetails.Find(id);
                orderDetail.Status = "done";
                Order order = db.Orders.Find(Oid);
                List<OrderDetail> orderDetailList = db.OrderDetails.Where(x => x.Oid == Oid).ToList();
                if (!orderDetailList.Exists(x=>x.Status=="ready"))
                {
                    if (!orderDetailList.Exists(x => x.Status == "prepare"))
                    {
                        order.Status = "done";
                    }
                    else
                    {
                        order.Status = "prepare";
                    }
                }
                
                db.SaveChanges();
                var result = db.OrderDetails.Where(x => x.Oid == Oid).Select(x => new
                {
                    pid = x.Pid,
                    name = x.ProductList.Name,
                    img = x.ProductList.ProductImg.Select(w=>w.Pimg).Take(1),
                    options = x.Options,
                    Qty = x.Qty,
                    subtotal = x.Qty * x.Price,
                    status = x.Status
                });
                return Content(JsonConvert.SerializeObject(result));
            }
            catch
            {
                return Content("fail");
            }
        }
        #endregion

        #region 40.總頁數GET
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
                total = x.OrderDetails.Sum(w => w.Price * w.Qty),
                status = x.Status
            });

            bool isTable = false;
            if (type == "forhere")
            {
                isTable = true;
            }

            if (!string.IsNullOrEmpty(type))
            {
                result = result.Where(x => x.isTable == isTable);
            }
            if (!string.IsNullOrEmpty(status))
            {
                if (status == "ready")
                {
                    result = result.Where(x => x.status == "ready" || x.status == "finish");
                }
                result = result.Where(x => x.status == status);
            }
            else
            {
                result = result.Where(x => x.status != "cancel" && x.status != "paid");
            }
            int page = result.Count();
            if (page % 9 == 0)
            {
                page = page / 9;
            }
            page = (page / 9) + 1;
            return Content(page.ToString());
        }
        #endregion

        #region 41.整張單送餐完畢GET
        public ActionResult OrderDelivered(int id)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                Order order = db.Orders.Find(id);
                order.Status = "done";
                List<OrderDetail> orderDetail = db.OrderDetails.Where(x => x.Oid == id).ToList();
                foreach (var item in orderDetail)
                {
                    item.Status = "done";
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

        #region 44.復原結帳單GET

        public ActionResult BackToDone(int Oid)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                var order = db.Orders.Find(Oid);
                order.Status = "done";
                db.SaveChanges();
                return Content("success");
            }
            catch 
            {
                return Content("fail");
            }
        }
        #endregion

        #region 44.復原取消單GET

        public ActionResult BackToPrepare(int Oid)
        {
            try
            {
                if (Session["EmployeeID"] == null)
                {
                    return Content("未登入");
                }
                var order = db.Orders.Find(Oid);
                order.Status = "prepare";
                List<OrderDetail> orderDetail = db.OrderDetails.Where(x => x.Oid == Oid).ToList();
                foreach (var item in orderDetail)
                {
                    item.Status = "prepare";
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
