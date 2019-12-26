using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using MvcPaging;
using sys.Filters;
using sys.Models;

namespace sys.Areas.Admin.Controllers
{
    [PermissionFilter]
    [Authorize]
    public class BKBannersController : Controller
    {
        private Membersql db = new Membersql();
        private const int PageSize = 5;
        // GET: Admin/BKBanners
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
            return View(db.Banners.OrderByDescending(x=>x.EndTime>DateTime.Now).ThenBy(x=>x.EndTime).ToList().ToPagedList((int)page, PageSize));
        }

        // GET: Admin/BKBanners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BKBanners/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Img,StartTime,EndTime")] Banner banner, HttpPostedFileBase Img)
        {
            if (ModelState.IsValid)
            {
                if (banner.StartTime>banner.EndTime)
                {
                    ViewBag.Message = "起始日期不能大於結束日期";
                    ViewBag.StartTime = banner.StartTime.ToString("yyyy-MM-dd");
                    ViewBag.EndTime = banner.EndTime.ToString("yyyy-MM-dd");
                    return View(banner);
                }

                if (Img != null)
                {
                    if (Img.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "檔案型態錯誤!";
                        return View(banner);
                    }
                    //取得副檔名
                    string extension = Path.GetExtension(Img.FileName);
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("/Img/BannerImg"), fileName);
                    Img.SaveAs(savedName);
                    banner.Img = fileName;
                }
                db.Banners.Add(banner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(banner);
        }

        // GET: Admin/BKBanners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Banner banner = db.Banners.Find(id);
            ViewBag.Img = banner.Img;
            ViewBag.StartTime = banner.StartTime.ToString("yyyy-MM-dd");
            ViewBag.EndTime = banner.EndTime.ToString("yyyy-MM-dd");
            return View(banner);
        }

        // POST: Admin/BKBanners/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Img,StartTime,EndTime")] Banner banner, HttpPostedFileBase NewImg)
        {
            //ModelState.Remove("Img");
            if (ModelState.IsValid)
            {
                if (banner.StartTime>banner.EndTime)
                {
                    ViewBag.Message = "開始日期不得大於結束日期!";
                    ViewBag.Img = banner.Img;
                    ViewBag.StartTime = banner.StartTime.ToString("yyyy-MM-dd");
                    ViewBag.EndTime = banner.EndTime.ToString("yyyy-MM-dd");
                    return View(banner);
                }
                if (NewImg != null)
                {
                    if (NewImg.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.ImgMessage = "檔案型態錯誤!";
                        ViewBag.Img = banner.Img;
                        ViewBag.StartTime = banner.StartTime.ToString("yyyy-MM-dd");
                        ViewBag.EndTime = banner.EndTime.ToString("yyyy-MM-dd");
                        return View(banner);
                    }
                    //取得副檔名
                    string extension = Path.GetExtension(NewImg.FileName);
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("/Img/BannerImg"), fileName);
                    NewImg.SaveAs(savedName);
                    banner.Img = fileName;
                }

                db.Entry(banner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        // POST: Admin/BKBanners/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Banner banner = db.Banners.Find(id);
            db.Banners.Remove(banner);
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
