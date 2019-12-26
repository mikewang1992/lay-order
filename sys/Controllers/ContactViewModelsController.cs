using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sys.Models;

namespace sys.Controllers
{
    public class ContactViewModelsController : Controller
    {
        private Membersql db = new Membersql();

        // GET: ContactViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactViewModels/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Account,Email,Tel,memo")] ContactViewModel contactViewModel)
        {
            if (ModelState.IsValid)
            {
                //todo:send email
                
                return RedirectToAction("Index","Home");
            }

            return View(contactViewModel);
        }

    }
}
