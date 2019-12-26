using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using sys.Filters;
using sys.Models;

namespace sys.Areas.Admin.Controllers
{
    [PermissionFilter]
    [Authorize]
    public class BKMembersController : Controller
    {
        private Membersql db = new Membersql();
        private const int PageSize = 10;
        // GET: Members
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
            return View(db.Members.ToList().ToPagedList((int)page, PageSize));
        }

        // GET: Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            ViewBag.id = id;
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        public ActionResult Create()
        {
            //有用jquery-tree時用
            List<Permission> permission = db.Permissions.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            getList(permission, stringBuilder);
            ViewBag.data = stringBuilder.ToString();
            return View();
        }

        public void getList(List<Permission> permission, StringBuilder stringBuilder)
        {
            foreach (var item in permission)
            {
                if (stringBuilder.ToString().IndexOf("{id:" + item.id) == -1)
                {
                    stringBuilder.Append("{id:" + item.id + ",text:'" + item.id + "." + item.PermissionName + "'");
                    if (permission.Where(x => x.Pid == item.id).Count() > 0)
                    {
                        stringBuilder.Append(",children:[");
                        var subP = permission.Where(x => x.Pid == item.id).ToList();
                        getList(subP, stringBuilder);
                        stringBuilder.Append("]");
                    }
                    stringBuilder.Append("},");
                }
            }
        }
        public void getList(List<Permission> permission, StringBuilder stringBuilder, string ownPermission)
        {
            foreach (var item in permission)
            {
                string check = "false";
                if (stringBuilder.ToString().IndexOf("{id:" + item.id) == -1)
                {
                    if (!string.IsNullOrEmpty(ownPermission))
                    {
                        if (ownPermission.IndexOf("," + item.id + ",") != -1)
                        {
                            check = "true";
                        }
                    }
                    else
                    {
                        check = "false";
                    }
                    stringBuilder.Append("{id:" + item.id + ",text:'" +item.id+"." +item.PermissionName + "'" + ",checked:" + check);
                    if (permission.Where(x => x.Pid == item.id).Count() > 0)
                    {
                        stringBuilder.Append(",children:[");
                        var subP = permission.Where(x => x.Pid == item.id).ToList();
                        getList(subP, stringBuilder, ownPermission);
                        stringBuilder.Append("]");
                    }
                    stringBuilder.Append("},");
                }

            }
        }
        // POST: Members/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,Password,PasswordSalt,Name,Gender,Email,Permission,Tel,initDate")] Member member, HttpPostedFileBase NewImg)
        {
            if (ModelState.IsValid)
            {
                if (NewImg != null)
                {
                    if (NewImg.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "照片型態錯誤!";
                        return View(member);
                    }

                    //取得副檔名
                    string extension = Path.GetExtension(NewImg.FileName);
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("/Admin/UploadFile/Img"), fileName);
                    NewImg.SaveAs(savedName);
                    member.Img = fileName;
                }
                else
                {
                    member.Img = "0.png";
                }
                //密碼加密
                string salt = Guid.NewGuid().ToString();
                member.Password = GenerateHashWithSalt(member.Password, salt);
                member.PasswordSalt = salt;
                member.initDate = DateTime.Now;
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            List<Permission> permission = db.Permissions.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            getList(permission, stringBuilder);
            ViewBag.data = stringBuilder.ToString();

            return View(member);
        }
        public static string GenerateHashWithSalt(string password, string salt)
        {
            // merge password and salt together
            string sHashWithSalt = password + salt;
            // convert this merged value to a byte array
            byte[] saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            // use hash algorithm to compute the hash
            HashAlgorithm algorithm = new SHA256Managed();
            // convert merged bytes to a hash as byte array
            byte[] hash = algorithm.ComputeHash(saltedHashBytes);
            // return the has as a base 64 encoded string
            return Convert.ToBase64String(hash);
        }

        // GET: Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            ViewBag.PWD = member.Password;
            ViewBag.salt = member.PasswordSalt;
            ViewBag.Img = member.Img;
            List<Permission> permission = db.Permissions.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            string ownPermission = member.Permission;
            getList(permission, stringBuilder, ownPermission);
            ViewBag.data = stringBuilder.ToString();
            ViewBag.Permission = member.Permission;
            ViewBag.initDate = member.initDate;
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,Password,PasswordSalt,Name,Gender,Email,Permission,Tel,Img,initDate")] Member member, HttpPostedFileBase NewImg, string NewPassword)
        {
            ViewBag.PWD = member.Password;
            ViewBag.salt = member.PasswordSalt;
            ViewBag.Img = member.Img;
            List<Permission> permission = db.Permissions.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            string ownPermission = member.Permission;
            getList(permission, stringBuilder, ownPermission);
            ViewBag.data = stringBuilder.ToString();
            ViewBag.Permission = member.Permission;
            ViewBag.initDate = member.initDate;
            if (ModelState.IsValid)
            {
                if (NewImg != null)
                {
                    if (NewImg.ContentType.IndexOf("image", System.StringComparison.Ordinal) == -1)
                    {
                        ViewBag.Message = "照片型態錯誤!";
                        return View(member);
                    }
                    //取得副檔名
                    string extension = Path.GetExtension(NewImg.FileName);
                    //新檔案名稱
                    string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, extension);
                    string savedName = Path.Combine(Server.MapPath("/Admin/UploadFile/Img"), fileName);
                    NewImg.SaveAs(savedName);
                    member.Img = fileName;
                    ViewBag.Img = member.Img;
                }
                //密碼加密
                if (!string.IsNullOrEmpty(NewPassword))
                {
                    string salt = Guid.NewGuid().ToString();
                    member.Password = GenerateHashWithSalt(NewPassword, salt);
                    member.PasswordSalt = salt;
                    ViewBag.PWD = member.Password;
                    ViewBag.salt = member.PasswordSalt;
                }
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                //ViewBag.PWD = member.Password;
                //ViewBag.salt = member.PasswordSalt;
                //ViewBag.Img = member.Img;
                //List<Permission> permission = db.Permissions.ToList();
                //StringBuilder stringBuilder = new StringBuilder();
                //string ownPermission = member.Permission;
                //getList(permission, stringBuilder, ownPermission);
                //ViewBag.data = stringBuilder.ToString();
                //ViewBag.Permission = member.Permission;
                //ViewBag.initDate = member.initDate;
                return View(member);
            }

            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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


        #region 驗證

        /// <summary>
        /// 產生驗證碼並寄發簡訊
        /// </summary>
        //public ActionResult SendSMS([Bind(Include = "Id,Account,Password,PasswordSalt,Name,Gender,Email,Permission,tel")] Member member)
        //{
        //    //隨機產生6位數字
        //    Random vertifyN = new Random();
        //    int vertify = vertifyN.Next(100000, 999999);
        //    string msg = "你好，您在lay-order的帳號驗證碼為" + vertify + "。請於驗證頁面輸入";
        //    string url = "https://api.every8d.com/API21/HTTP/sendSMS.ashx?UID=0932961027&PWD=layorder&SB=vertify&MSG="+msg+"&DEST="+member.Tel+"&ST=";
        //    if (GetVertifyNumber(url).StartsWith("-"))
        //    {
        //        //todo:回到錯誤頁??
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        //存到資料庫
        //        member.Account = vertify;
        //            db.Members.Add(member);

        //        db.SaveChanges();
        //        //todo:回到驗證頁??
        //        return RedirectToAction("Index");
        //    }

        //}
        //private static string GetVertifyNumber(string Url)
        //{
        //    try
        //    {
        //        //string targetURI = Url;
        //        var request = WebRequest.Create(Url);
        //        string text;
        //        var response = (HttpWebResponse)request.GetResponse();
        //        using (var sr = new StreamReader(response.GetResponseStream()))
        //        {
        //            text = sr.ReadToEnd();
        //        }

        //        return text;
        //    }
        //    catch (Exception)
        //    {
        //        return string.Empty;
        //    }
        //}

        #endregion
    }
}
