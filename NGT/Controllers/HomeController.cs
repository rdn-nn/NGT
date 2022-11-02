using NGT.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NGT.Controllers
{
    public class HomeController : Controller
    {
        private NgtContexto db = new NgtContexto();
        public ActionResult Index()
        {
            return View();
        }
    }
}