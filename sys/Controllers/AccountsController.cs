using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace sys.Models
{
    public class AccountsController : Controller
    {
        private Membersql db = new Membersql();

        public ActionResult Index()
        {
            return Redirect("https://lay-order.rocket-coding.com/index.html");
        }

        #region 20.註冊API/POST
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tel,Password,PasswordSalt,Name,Birth,County,Dist,Vertify,Check,IsTable")] Account account)
        {
            try
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(account.Tel, @"^09[0-9]{8}$")==false)
                {
                    return Content("非手機電話號碼");
                }
                if (db.Accounts.Where(x => x.Tel == account.Tel).Count() > 0)
                {
                    return Content("此電話已存在，請勿重複申請");
                }
                //產生密碼鹽以及密碼加密
                string salt = Guid.NewGuid().ToString();
                account.Password = GenerateHashWithSalt(account.Password, salt);
                account.PasswordSalt = salt;
                //寄發SMS
                string text = SendSMS(account.Tel);
                if (text == "fail")
                {
                    return Content("fail");
                }
                account.Vertify = text;
                //記住總共發過幾次簡訊
                account.Sent = 1;
                //輸錯驗證次數預設0
                account.wrong = 0;
                db.Accounts.Add(account);
                db.SaveChanges();
                //存註冊ID給驗證API用
                //Session["RegisteredId"] = account.Id.ToString();
                //HttpCookie RegisteredId = new HttpCookie("RegisteredId");
                //RegisteredId.Value= account.Id.ToString();
                //Response.Cookies.Add(RegisteredId);
                return Content("success");
            }
            catch(Exception)
            {
                return Content("fail");
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

        #region 驗證碼

        /// <summary>
        /// 產生驗證碼並寄發簡訊
        /// </summary>
        public string SendSMS(string Tel)
        {
            //隨機產生6位數字變成驗證碼
            Random vertifyN = new Random();
            int vertify = vertifyN.Next(100000, 999999);
            //組成簡訊內容並寄發
            string msg = "你好，您在lay-order的帳號驗證碼為" + vertify + "。請於驗證頁面輸入";
            msg = HttpUtility.UrlEncode(msg);
            string url = "http://api.every8d.com/API21/HTTP/sendSMS.ashx?UID=0980197363&PWD=uv3g&SB=vertify&MSG=" + msg + "&DEST=" + Tel + "&ST=";
            string text = GetAPIResponse(url);
            //判斷回傳值
            if (text.StartsWith("-"))
            {
                return "fail";
            }
            else
            {
                return vertify.ToString();
            }
        }
        //戳簡訊API
        private static string GetAPIResponse(string Url)
        {
            try
            {
                var request = WebRequest.Create(Url);
                string text;
                var response = (HttpWebResponse)request.GetResponse();
                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    text = sr.ReadToEnd();
                }
                return text;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #endregion
        #endregion

        #region 18.確認驗證碼POST
        public ActionResult Vertify(string Vertify,string Tel)
        {
            //HttpCookie RegisteredId = Request.Cookies["RegisteredId"];
            //int id = Convert.ToInt32(RegisteredId.Value);
            //Account account = db.Accounts.Find(id);
            Account account = db.Accounts.Where(x => x.Tel == Tel).FirstOrDefault();
            
            if (db.Accounts.Where(x => x.Tel==Tel && x.Vertify == Vertify).Count() > 0)
            {
                account.IsCheck = true;
                account.Sent = 0;
                account.wrong = 0;
                db.SaveChanges();
                Session["Id"] = account.Id;
                Session["IsTable"] = account.IsTable;
                Session["Tel"] = account.Tel;
                Session["Name"] = account.Name;
                return Content(account.Id.ToString());
            }
            else
            {
                int fail = account.wrong;
                fail = fail + 1;
                account.wrong = fail;
                if (fail==3)
                {
                    account.Vertify = 0.ToString();
                    account.wrong = 0;
                    db.SaveChanges();
                    return Content("驗證碼輸入失敗3次，請重新取得驗證碼");
                }
                db.SaveChanges();
                return Content("驗證失敗，請重新輸入");
            }

        }
        #endregion

        #region 19.重新寄發驗證碼GET
        public ActionResult ReSendSMS(string Tel)
        {
            try
            {
                //HttpCookie RegisteredId = Request.Cookies["RegisteredId"];
                //int id = Convert.ToInt32(RegisteredId.Value);
                //Account account = db.Accounts.Find(id);
                Account account = db.Accounts.Where(x => x.Tel == Tel).FirstOrDefault();
                int sent = account.Sent;
                
                if (sent == 3)
                {
                    return Content("已寄發3次驗證碼，請您再次確認電話是否正確");
                }

                //隨機產生6位數字變成驗證碼
                Random vertifyN = new Random();
                int vertify = vertifyN.Next(100000, 999999);
                //組成簡訊內容並寄發
                string msg = "你好，您在lay-order的帳號驗證碼為" + vertify + "。請於驗證頁面輸入";
                msg = HttpUtility.UrlEncode(msg);
                string url =
                    "http://api.every8d.com/API21/HTTP/sendSMS.ashx?UID=0980197363&PWD=uv3g&SB=vertify&MSG=" + msg +
                    "&DEST=" + account.Tel + "&ST=";
                string text = GetAPIResponse(url);
                //判斷回傳值
                if (text.StartsWith("-"))
                {
                    return Content("fail");
                }
                else
                {
                    account.Vertify = vertify.ToString();
                    account.Sent = sent + 1;
                    db.SaveChanges();
                    return Content("success");
                }
            }
            catch
            {
                return Content("fail");
            }
           
        }
        #endregion

        #region 優惠劵GET
        public ActionResult Voucher()
        {
            DateTime now= DateTime.UtcNow.AddHours(08);
            return Content(JsonConvert.SerializeObject(db.Vouchers.Where(x => x.EndTime > now && x.StartTime < now).ToList()));
        }
        #endregion

        #region 登入POST
        [HttpPost]
        public ActionResult Login(string Tel,string Password)
        {
            Account account =db.Accounts.SingleOrDefault(x=>x.Tel==Tel);
            if (account == null)
            {
                return Content("此帳號不存在");
            }
            Password = GenerateHashWithSalt(Password, account.PasswordSalt);
            //進行比對
            if (db.Accounts.Where(x => x.Tel == Tel && x.Password == Password && x.IsCheck==true).Count()>0)
            {
                //Session["Login"] = true;
                Session["IsTable"] = account.IsTable;
                Session["Id"] = account.Id;
                Session["Tel"] = account.Tel;
                Session["Name"] = account.Name;
                return Content("success");
            }
            if (db.Accounts.Where(x => x.Tel == Tel && x.Password == Password && x.IsCheck==false).Count() > 0)
            {
                return Content("此電話號碼尚未進行驗證");
            }
            return Content("登入失敗");
        }
        #endregion

        #region 檢查是否登入/GET
        public ActionResult CheckLogin()
        {
            if (Session["Id"] != null)
            {
                return Content("True");
            }

            return Content("False");
        }
        #endregion

        #region 內用外帶GET
        public ActionResult IsTable()
        {
            if (Session["IsTable"] != null)
            {
                if (Session["IsTable"].ToString() == "True")
                {
                    return Content("內用");
                }
            }
            return Content("外帶");
        }


        #endregion

        #region 顯示會員資料GET
        public ActionResult ShowUser()
        {
            if (Session["Id"] == null)
            {
                return Content("未登入");
            }
            int id = Convert.ToInt32(Session["Id"]);
            return Content(JsonConvert.SerializeObject(db.Accounts.Where(x=>x.Id==id).Select(x=>new {x.Tel,x.Name,x.Birth,x.County,x.Dist})));
        }
        #endregion

        #region 修改會員資料POST
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(string Name, string NewPassword, string Birth, string County, string Dist)
        {
            try
            {
                if (Session["Id"] == null)
                {
                    return Content("未登入");
                }

                int id = Convert.ToInt32(Session["Id"]);
                Account account = db.Accounts.Find(id);
                account.Name = Name;
                account.Birth = Birth;
                account.County = County;
                account.Dist = Dist;
                if (!string.IsNullOrEmpty(NewPassword))
                {
                    string salt = Guid.NewGuid().ToString();
                    account.Password = GenerateHashWithSalt(NewPassword, salt);
                    account.PasswordSalt = salt;
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

        #region 點餐頁面會員資料GET
        public ActionResult OrderMember()
        {
            if (Session["Id"] == null)
            {
                return Content("");
            }
            string Tel = Session["Tel"].ToString();
            string Name = Session["Name"].ToString();
            return Content(Tel + "," + Name);
        }
        #endregion

        #region 37.員工登入POST

        [HttpPost]
        public ActionResult EmployeeLogin(string id, string Password)
        {
            try
            {
                Member member = db.Members.FirstOrDefault(x => x.Account == id);
                if (member == null)
                {
                    return Content("fail");
                }
                string salt = member.PasswordSalt;
                string typePassword = GenerateHashWithSalt(Password, salt);
                if (typePassword != member.Password)
                {
                    return Content("fail");
                }
                else
                {
                    Session["EmployeeID"] = id;
                    return Content("success");
                }
                return Content("fail");
            }
            catch
            {
                return Content("fail");
            }
            
        }

        #endregion

        #region 26.忘記密碼後修改密碼
        [HttpPost]
        public ActionResult EditPassword(int Id, string Password)
        {
            try
            {
                Account account = db.Accounts.Find(Id);
                string salt = Guid.NewGuid().ToString();
                account.Password = GenerateHashWithSalt(Password, salt);
                account.PasswordSalt = salt;
                db.SaveChanges();
                return Content("succcess");
            }
            catch (Exception e)
            {
                return Content("fail");
            }
           
        }


        #endregion

        #region 29.機器人
        [HttpPost]
        public ActionResult Robot(string hiddenToken)
        {
            string token = hiddenToken;
            string tokencContent = PostJsonContent(token);
            //取得隱藏欄位token
            ResponseToken responseToken = JsonConvert.DeserializeObject<ResponseToken>(tokencContent);
            Session["verification"] = responseToken.success;
            if (responseToken.success != true)
            {
                return Content("機器人來襲");
            }
            return Content("success");
        }
        private static string PostJsonContent(string token)
        {
            string key = "6LexvsYUAAAAAB3gHcOyidlTpBMe4rszImHdAc4G";
            try
            {
                WebRequest request = HttpWebRequest.Create("https://www.google.com/recaptcha/api/siteverify");
                request.Method = "POST";
                //使用 application/x-www-form-urlencoded
                request.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

                //要傳送的資料內容(依字串表示)
                string postData = $"secret=6LexvsYUAAAAAB3gHcOyidlTpBMe4rszImHdAc4G&response={token}";
                //將傳送的字串轉為 byte array
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                //告訴 server content 的長度
                request.ContentLength = byteArray.Length;
                //將 byte array 寫到 request stream 中 
                using (Stream reqStream = request.GetRequestStream())
                {
                    reqStream.Write(byteArray, 0, byteArray.Length);
                }
                using (var httpResponse = (HttpWebResponse)request.GetResponse())
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }

            }

            catch (Exception)
            {
                return string.Empty;
            }
        }

        public class ResponseToken
        {
            public DateTime challenge_ts { get; set; }
            public float score { get; set; }
            public string action { get; set; }
            public bool success { get; set; }
            public string hostname { get; set; }
        }
        #endregion

        #region 42.會員登出GET

        public ActionResult Logout()
        {
            Session.Remove("IsTable");
            Session.Remove("Id");
            Session.Remove("Tel");
            Session.Remove("Name");
            return Content("success");
        }
        #endregion
        #region 43.員工登出GET

        public ActionResult EmployeeLogout()
        {
            Session.Remove("EmployeeID");
            
            return Content("success");
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
