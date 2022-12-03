using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGT.Areas.Admin.Controllers
{
    public class ItemController : Controller
    {
        // GET: Admin/Item
        public ActionResult Index()
        {
            return View();
        }
    }
}