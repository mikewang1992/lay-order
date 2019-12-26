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
using sys.Filters;
using sys.Models;

namespace sys.Areas.Admin.Controllers
{
    [PermissionFilter]
    [Authorize]
    public class BKOrdersController : Controller
    {
        private Membersql db = new Membersql();
        private const int PageSize = 10;
        // GET: Admin/BKOrders
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
            var orders = db.Orders.Include(o => o.Account).Include(x => x.OrderDetails)
                .OrderByDescending(x => x.GetTime);
            return View(orders.ToList().ToPagedList((int)page, PageSize));
        }
        [HttpPost]
        public ActionResult Index(int? page,string Tel,string status, DateTime? StartTime,DateTime? EndTime,string isTable)
        {
            if (!page.HasValue)
            {
                page = 0;
            }
            else
            {
                page--;//ToPagedList的pageIndex預設第一頁是0,第二頁是1，所以要-1才是真的頁面
            }
            var orders = db.Orders.Include(o => o.Account).Include(x => x.OrderDetails)
                .OrderByDescending(x => x.GetTime).AsQueryable();
            if (!string.IsNullOrEmpty(Tel))
            {
                orders = orders.Where(x => x.Account.Tel.Contains(Tel));
            }
            if (status!= "請選擇")
            {
                orders = orders.Where(x => x.Status.Contains(status));
            }
            if (StartTime!=null)
            {
                orders = orders.Where(x => x.GetTime> StartTime);
            }
            if (EndTime != null)
            {
                orders = orders.Where(x => x.GetTime < EndTime);
            }
            if(isTable != "請選擇")
            {
                bool table = Convert.ToBoolean(isTable);
                orders = orders.Where(x => x.Account.IsTable== table);
            }
            return View(orders.ToList().ToPagedList((int)page, PageSize));
        }
        // GET: Admin/BKOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<OrderDetail> orderDetail = db.OrderDetails.Where(x => x.Oid == id).ToList();
            return View(orderDetail);
        }

        //public ActionResult Search()
        //{
            
        //}

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
