using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MvcPaging;
using sys.Filters;
using sys.Models;

namespace sys.Areas.Admin.Controllers
{
    [PermissionFilter]
    [Authorize]
    public class BKTableController : Controller
    {
        private Membersql db = new Membersql();
        private const int PageSize = 10;
        // GET: Admin/BKTable
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
            return View(db.Accounts.Where(x => x.IsTable == true).OrderByDescending(x => x.Id).ToList().ToPagedList((int)page, PageSize));
        }

        // GET: Admin/BKTable/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tel,Password,PasswordSalt,Name,Birth,County,Dist,Vertify,IsCheck,IsTable,Sent,wrong")] Account account)
        {
            try
            {
                //if (System.Text.RegularExpressions.Regex.IsMatch(account.Tel, @"^09[0-9]{8}$") == false)
                //{
                //    return Content("非手機電話號碼");
                //}
                if (db.Accounts.Where(x => x.Tel == account.Tel).Count() > 0)
                {
                    ViewBag.Message = "此帳號已存在，請勿重複設定";
                    return View(account);
                }
                //產生密碼鹽以及密碼加密
                string urlPWD = account.Password;
                string salt = Guid.NewGuid().ToString();
                account.Password = GenerateHashWithSalt(account.Password, salt);
                account.PasswordSalt = salt;
                account.Sent = 0;
                account.wrong = 0;
                account.IsTable = true;
                account.IsCheck = true;
                string data = "https://lay-order.rocket-coding.com/index.html#/login?" + urlPWD + "&" + account.Tel;
                data = HttpUtility.UrlEncode(data);
                string Url = "http://www.funcode-tech.com/Encoder_Service/img.aspx?custid=1&username=public&codetype=QR&EClevel=0&data=" + data;
                var QRCode = GetQRCode(Url);
                account.Vertify = QRCode;
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View(account);
            }
        }
        //public HttpResponseMessage GetQrCode()
        //{
        //    var imgPath = @"D:\ITdosCom\Images\itdos.jpg";
        //    //从图片中读取byte  
        //    var imgByte = File.ReadAllBytes(imgPath);
        //    //从图片中读取流  
        //    var imgStream = new MemoryStream(File.ReadAllBytes(imgPath));
        //    var resp = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new ByteArrayContent(imgByte)
        //        //或者  
        //        //Content = new StreamContent(stream)  
        //    };
        //    resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
        //    return resp;
        //}
        //[Route("transfer")]
        //[HttpGet]
        //public async Task<HttpResponseMessage> TransferAsync(string url)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    return await httpClient.GetAsync(url);
        //}
        //private static string GetAPIResponse(string Url)
        //{
        //    try
        //    {
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
        private static string GetQRCode(string Url)
        {
            try
            {
                WebClient WebWay = new WebClient();
                string fileName = String.Format("{0:yyyyMMddhhmmsss}.{1}", DateTime.Now, "png");
                string FilePath = System.Web.HttpContext.Current.Server.MapPath(String.Format("/Admin/UploadFile/Img/{0}", fileName)); //存檔路徑
                WebWay.DownloadFile(Url, FilePath); //利用WebWay這個方法下載至本機(下載路徑,存檔路徑)
                return fileName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        #region 密碼加密
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
        #endregion
        // GET: Admin/BKTable/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            ViewBag.PWD = account.Password;
            ViewBag.salt = account.PasswordSalt;
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tel,Password,PasswordSalt,Name,Birth,County,Dist,Vertify,IsCheck,IsTable,Sent,wrong")] Account account, string NewPassword)
        {
            if (ModelState.IsValid)
            {
                string urlPWD = account.Password;
                if (db.Accounts.Where(x => x.Tel == account.Tel).Count() > 0)
                {
                    ViewBag.Message = "此帳號已存在，請勿重複設定";
                    return View(account);
                }
                //密碼加密
                if (!string.IsNullOrEmpty(NewPassword))
                {
                    string salt = Guid.NewGuid().ToString();
                    account.Password = GenerateHashWithSalt(NewPassword, salt);
                    account.PasswordSalt = salt;
                }
                account.IsTable = true;
                account.IsCheck = true;
                string data = "https://lay-order.rocket-coding.com/index.html#/login?" + urlPWD + "&" + account.Tel;
                data = HttpUtility.UrlEncode(data);
                string Url = "http://www.funcode-tech.com/Encoder_Service/img.aspx?custid=1&username=public&codetype=QR&EClevel=0&data="+data;
                var QRCode = GetQRCode(Url);
                account.Vertify = QRCode;
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        // POST: Admin/BKTable/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult QRCode(int id)
        {
            Account table = db.Accounts.Find(id);
            ViewBag.url = table.Vertify;
            return View();
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
