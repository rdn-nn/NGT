using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NGT.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View("Dashboard");
        }



        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home.Index");
        }
    }
}