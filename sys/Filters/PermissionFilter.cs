using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using sys.Models;

namespace sys.Filters
{
    public class PermissionFilter: ActionFilterAttribute //繼承ActionFilterAttribute實作自訂標籤
    {
        private Membersql db = new Membersql();
        public override void OnActionExecuting(ActionExecutingContext filterContext)//覆寫ActionFilterAttribute裡的OnActionExecuting，代表驗證後要做的事
        {
            //作帳號判斷確保已登入
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.ViewBag.menu = "";
                return;
            }
            //取得該頁面controllerName/actionName
            string controllerName = filterContext.Controller.ControllerContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.Controller.ControllerContext.RouteData.Values["action"].ToString();
            string strTicket = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            Member UPerson = JsonConvert.DeserializeObject<Member>(strTicket);
            //放圖片
            filterContext.Controller.ViewBag.userImg = UPerson.Img;
            //放名字
            filterContext.Controller.ViewBag.Name = UPerson.Name;
            //左側選單
            List<Permission> permission = db.Permissions.ToList();
            StringBuilder stringBuilder = new StringBuilder();
            string status = "";
            foreach (var item in permission)
            {
                if (UPerson.Permission.IndexOf("," + item.id + ",") > -1)
                {
                    if (item.Pid==null)
                    {
                        if (item.Controller == controllerName)
                        {
                            status = "active";
                        }
                        stringBuilder.Append(@"<li class="""+status+@"""><a href=""/Admin/" + item.Controller + "/" + item.Action + @"""><i class="""+ item.icon + @"""></i> <span>" + item.PermissionName + @"</a>");

                        //第一層
                        //stringBuilder.Append(@"<li class=""treeview ""><a href=""/" + item.Controller + "/" + item.Action + @"""><i class=""fa fa-dashboard""></i> <span>" + item.PermissionName + @"</span><span class=""pull-right-container""><i class=""fa fa-angle-left pull-right""></i></span></a>");
                        //第二層
                        //stringBuilder.Append(@"<ul class=""treeview-menu"">");
                        //foreach (var sub in permission.Where(x => x.Pid == item.id))
                        //{
                        //    stringBuilder.Append(@"<li><a href = ""/" + sub.Controller + "/" + sub.Action + @""" ><i class=""fa fa-circle-o""></i>" + sub.PermissionName + @"</a></li>");
                        //}
                        //stringBuilder.Append(@"</ul>");
                    }
                    //else
                    //{
                    //    stringBuilder.Append(@"<li class=""treeview ""><a href=""/" + item.Controller + "/" + item.Action + @"""><i class=""fa fa-dashboard""></i> <span>" + item.PermissionName + @"</a>");
                    //}
                    stringBuilder.Append(@"</li>");
                }
                status = "";
            }

            filterContext.Controller.ViewBag.Menu = stringBuilder.ToString();
        }
    }
}